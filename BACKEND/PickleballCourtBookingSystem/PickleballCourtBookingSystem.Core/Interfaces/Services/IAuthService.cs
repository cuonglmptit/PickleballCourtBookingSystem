using System.Security.Claims;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IAuthService
{
    public ServiceResult Register(string username, string password);
    public ServiceResult Login(string username, string password);
    public ClaimsPrincipal? ValidateToken(string token);
}