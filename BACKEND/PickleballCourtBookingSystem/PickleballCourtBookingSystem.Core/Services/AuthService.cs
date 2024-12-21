using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IAddressService _addressService;

    private ServiceResult CreateServiceResult(bool Success, int StatusCode, string? UserMsg = null,
        string? DevMsg = null, object? Data = null)
    {
        ServiceResult result = new ServiceResult();
        result.Success = Success;
        result.UserMsg = UserMsg;
        result.DevMsg = DevMsg;
        result.StatusCode = StatusCode;
        result.Data = Data;
        return result;
    }

    public AuthService(IConfiguration configuration, IUserRepository userRepository, IAddressService addressService)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _addressService = addressService;
    }

    // public ServiceResult Register(string username, string password, string name, string phoneNumber, string email, int role)
    // {
    //     try
    //     {
    //         var 
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Failed to add booking", DevMsg: e.Message);
    //     }
    // }

    public ServiceResult Login(string username, string password)
    {
        var user = _userRepository.CheckLogin(username, password);
        if (user == null)
        {
            return CreateServiceResult(Success: true, StatusCode: 401,
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
        return CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Đăng nhập thành công",
            DevMsg: "Đăng nhập thành công", Data: new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
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