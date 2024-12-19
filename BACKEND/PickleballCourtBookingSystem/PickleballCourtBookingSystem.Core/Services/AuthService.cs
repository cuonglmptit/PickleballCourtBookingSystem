using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

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

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public ServiceResult Register(string username, string password)
    {
        throw new NotImplementedException();
    }

    public ServiceResult Login(string username, string password)
    {
        var user = _userRepository.CheckLogin(username, password);
        if (user == null)
        {
            return CreateServiceResult(Success: true, StatusCode: 401,
                UserMsg: "Tài khoản hoặc mật khẩu đăng nhập không chính xác",
                DevMsg: "Tài khoản hoặc mật khẩu đăng nhập không chính xác");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
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
}