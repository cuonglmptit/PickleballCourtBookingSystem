﻿using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Common;
using PickleballCourtBookingSystem.Core.CustomValidation;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using System.Data;

namespace PickleballCourtBookingSystem.Core.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAddressService _addressService;
    public UserService(IUserRepository repository, IUserRepository userRepository, IAddressService addressService) : base(repository)
    {
        _userRepository = userRepository;
        _addressService = addressService;
    }

    public ServiceResult Register(User user, Dictionary<string, string>? errorData = null)
    {
        errorData ??= new Dictionary<string, string>();
        // 1. Check username valid
        if (!CustomValidationMethods.IsValidUsername(user.Username))
        {
            errorData.Add("Username", "Username không hợp lệ");
        }

        // 2. Check email valid
        if (!CustomValidationMethods.IsValidEmail(user.Email))
        {
            errorData.Add("Email", "Email không hợp lệ");
        }

        // 3. Check full name valid
        if (!CustomValidationMethods.IsValidFullName(user.Name))
        {
            errorData.Add("FullName", "Tên không hợp lệ");
        }

        // 4. Check role valid
        // Kiểm tra nếu RoleId có giá trị trước khi sử dụng
        if (user.RoleId == null || !Enum.IsDefined(typeof(RoleEnum), user.RoleId))
        {
            errorData.Add("Role", "Role không hợp lệ");
        }
        else
        {
            if ((RoleEnum)user.RoleId != RoleEnum.Customer && (RoleEnum)user.RoleId != RoleEnum.CourtOwner)
            {
                errorData.Add("Role", "Role không hợp lệ (chỉ được là CourtOwner hoặc Customer)");
            }
        }

        List<string> columsCheck = new List<string> { nameof(User.Username), nameof(User.Email), nameof(User.PhoneNumber) };
        List<string> duplicatedColumns = _userRepository.HasDuplicateValuesInOtherRecords(user, columsCheck);
        if (duplicatedColumns.Any())
        {
            foreach (var column in duplicatedColumns)
            {
                errorData.Add(column, column + " đã tồn tại");
            }
        }
        // 5. Nếu có bất kì lỗi nào thì return lỗi
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
        var insertRes = this.InsertService(user);

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
}