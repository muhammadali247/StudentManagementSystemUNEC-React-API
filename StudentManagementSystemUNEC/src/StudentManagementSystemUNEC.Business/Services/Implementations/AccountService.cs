using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.AccExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class AccountService /*: IAccountService*/
{
    private readonly IOTPService _otpService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IValidator<RegisterDTO> _registerValidator;

    public AccountService(IValidator<RegisterDTO> registerValidator, UserManager<AppUser> userManager, IMapper mapper, IOTPService otpService)
    {
        _registerValidator = registerValidator;
        _userManager = userManager;
        _mapper = mapper;
        _otpService = otpService;
    }

    //public async Task<string> RegisterAsync(RegisterDTO registerDTO)
    //{
    //    var validationResult = await _registerValidator.ValidateAsync(registerDTO);
    //        if (!validationResult.IsValid) throw new RegisterFailException("Validation error(s) reported");

    //    AppUser user = await _userManager.FindByNameAsync(registerDTO.UserName);
    //        if (user != null) throw new UserWithSameUsernameExists("User with same username already exists");

    //    user = await _userManager.FindByNameAsync(registerDTO.Email);
    //        if (user != null) throw new UserWithSameEmailExists("User with same email already exists");

    //    var newUser = _mapper.Map<AppUser>(registerDTO);

    //    // Generate OTP and set expiry date
    //    var otp = _otpService.GenerateOTP();
    //    newUser.OTP = otp;
    //    newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);
    //}
}