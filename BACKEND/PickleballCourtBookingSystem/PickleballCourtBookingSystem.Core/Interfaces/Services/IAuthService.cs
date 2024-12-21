using System.Security.Claims;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IAuthService
{
    public ServiceResult Register(string username, string password, string confirmPassword, string fullName, string phoneNumber, string email, RoleEnum role);
    public ServiceResult Login(string username, string password);
    public ClaimsPrincipal? ValidateToken(string token);
    public Dictionary<string, string> GetClaimsFromToken(string token);
    public string? GetUserIdFromToken(string token);
}