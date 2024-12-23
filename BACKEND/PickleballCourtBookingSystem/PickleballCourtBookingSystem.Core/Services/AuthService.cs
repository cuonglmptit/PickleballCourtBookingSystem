using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Common;
using PickleballCourtBookingSystem.Core.CustomValidation;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IAddressService _addressService;
    private readonly IUserService _userService;

    public AuthService(IConfiguration configuration, IUserRepository userRepository, IAddressService addressService, IUserService userService)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _addressService = addressService;
        _userService = userService;
    }

    public ServiceResult Register(string username, string password, string confirmPassword, string fullName, string phoneNumber, string email, RoleEnum role)
    {
        var errorData = new Dictionary<string, string>();

        try
        {
            // 1. Check username valid
            if (!CustomValidationMethods.IsValidUsername(username))
            {
                errorData.Add("Username", "Username không hợp lệ");
            }

            // 2. Check confirm password
            if (!password.Equals(confirmPassword))
            {
                errorData.Add("ConfirmPassword", "Password confirm không trùng khớp");
            }

            // 3. Check email valid
            if (!CustomValidationMethods.IsValidEmail(email))
            {
                errorData.Add("Email", "Email không hợp lệ");
            }

            // 4. Check full name valid
            if (!CustomValidationMethods.IsValidFullName(fullName))
            {
                errorData.Add("FullName", "Tên không hợp lệ");
            }

            // 5. Check role valid
            if (role != RoleEnum.Customer && role != RoleEnum.CourtOwner)
            {
                errorData.Add("Role", "Role không hợp lệ (chỉ được là CourtOwner hoặc Customer)");
            }

            // 6. Check username tồn tại
            var user = new User
            {
                //****note quan trọng, bắt buộc phải có guid mới có thể check unique
                // không thì nó sẽ là null và kết qua trả về sẽ sai
                // null đại diện cho một giá trị không xác định, và bất kỳ phép so sánh nào với null,
                // bao gồm !=, đều trả về giá trị false
                Id = Guid.Empty,
                Username = username.Trim(),
                Password = password,
                Name = fullName,
                PhoneNumber = phoneNumber,
                Email = email,
                RoleId = (int)role
            };

            List<string> columsCheck = new List<string> { nameof(User.Username), nameof(User.Email), nameof(User.PhoneNumber) };
            List<string> duplicatedColumns = _userRepository.HasDuplicateValuesInOtherRecords(user, columsCheck);
            if (duplicatedColumns.Any())
            {
                foreach (var column in duplicatedColumns)
                {
                    errorData.Add(column, column + " đã tồn tại");
                }
            }
            // 7. Nếu có bất kì lỗi nào thì return lỗi
            if (errorData.Any())
            {
                return CommonMethods.CreateServiceResult(
                    Success: false,
                    StatusCode: 400,
                    UserMsg: "Validation errors occurred",
                    DevMsg: "Validation failed for one or more fields",
                    Data: errorData
                );
            }

            // 8. Success case: Register user
            user.Id = Guid.NewGuid();
            //Nếu user ok thì thêm address
            var newAddressId = Guid.NewGuid();
            _addressService.InsertService(
                new Address
                {
                    Id = newAddressId,
                    City = "",
                    District = "",
                    Street = "",
                    Ward = ""
                });

            //Gán newAddressId vào user
            user.AddressId = newAddressId;
            //Các thuộc tính khác của user
            user.IsActive = 1;
            var newCode = _userRepository.FindLargestValueEndsWithNumberInColumn(nameof(User.Code));
            user.Code = int.TryParse(newCode, out int parsedCode) ? parsedCode + 1 : 0;
            user.AvatarUrl = "";
            var insertRes = _userService.InsertService(user);

            if (insertRes.Success)
            {
                return CommonMethods.CreateServiceResult(
                    Success: true,
                    StatusCode: 201,
                    UserMsg: "Account created successfully",
                    DevMsg: "Account created successfully"
                );
            }
            else
            {
                return CommonMethods.CreateServiceResult(
                    Success: false,
                    StatusCode: 500,
                    UserMsg: "Failed to create account: " + insertRes.UserMsg,
                    DevMsg: "Failed to create account: " + insertRes.DevMsg
                );
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CommonMethods.CreateServiceResult(
                Success: false,
                StatusCode: 500,
                UserMsg: "Failed to create account",
                DevMsg: e.Message
            );
        }
    }

    public ServiceResult Login(string username, string password)
    {
        try
        {
            var user = _userRepository.CheckLogin(username, password);
            if (user == null)
            {
                return CommonMethods.CreateServiceResult(Success: true, StatusCode: 401,
                    UserMsg: "Tài khoản hoặc mật khẩu đăng nhập không chính xác",
                    DevMsg: "Tài khoản hoặc mật khẩu đăng nhập không chính xác");
            }
            var roleEnum = (RoleEnum)Enum.ToObject(typeof(RoleEnum), user.RoleId);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, roleEnum.ToString())
        };
            var token = GenerateToken(claims);
            return CommonMethods.CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Đăng nhập thành công",
                DevMsg: "Đăng nhập thành công", Data: new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CommonMethods.CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Login service exception", DevMsg: e.Message);
        }
    }

    public JwtSecurityToken GenerateToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var tokenExpirationMinutes = int.Parse(_configuration["Jwt:TokenExpirationMinutes"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes),
            SigningCredentials = new SigningCredentials(
                symmetricKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return token;
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = symmetricKey
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

            return principal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public Dictionary<string, string> GetClaimsFromToken(string token)
    {
        var claims = new Dictionary<string, string>();

        try
        {
            var principal = ValidateToken(token);

            if (principal == null)
                return claims;

            foreach (var claim in principal.Claims)
            {
                claims[claim.Type] = claim.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return claims;
    }

    public string? GetUserIdFromToken(string token)
    {
        try
        {
            var principal = ValidateToken(token);
            var userIdClaim = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}