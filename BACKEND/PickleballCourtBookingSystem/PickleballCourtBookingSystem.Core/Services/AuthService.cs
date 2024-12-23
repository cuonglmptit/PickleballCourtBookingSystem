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

        try
        {
            var errorData = new Dictionary<string, string>();
            // 0. Check confirm password
            if (!password.Equals(confirmPassword))
            {
                errorData.Add("ConfirmPassword", "Password confirm không trùng khớp");
            }
            //1. Tạo 1 user để thử đăng ký
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

            //Trả về kết quả đăng ký (cộng thêm các lỗi valid trước đó)
            return _userService.Register(user, errorData);

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