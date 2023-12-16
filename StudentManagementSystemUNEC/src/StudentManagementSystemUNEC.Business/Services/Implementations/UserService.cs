using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.AccExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.RoleExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.StudentExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.TeacherExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;
using StudentManagementSystemUNEC.Business.HelperServices.Implementations;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.Core.Utils.Enums;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IValidator<UserPostDTO> _userCreateValidator;
    private readonly IValidator<studentUserPostDTO> _studentUserCreateValidator;
    private readonly OTPService _otpService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IEmailService _emailService;
    private readonly IValidator<VerifyAccountDTO> _verificationValidator;
    private readonly IFileReadService _fileReadService;

    public UserService(ITeacherRepository teacherRepository, RoleManager<IdentityRole> roleManager, IStudentRepository studentRepository, AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, IValidator<UserPostDTO> userCreateValidator, OTPService otpService, IWebHostEnvironment webHostEnvironment, IEmailService emailService, IValidator<VerifyAccountDTO> verificationValidator, IFileReadService fileReadService, IValidator<studentUserPostDTO> studentUserCreateValidator)
    {
        _teacherRepository = teacherRepository;
        _roleManager = roleManager;
        _studentRepository = studentRepository;
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
        _userCreateValidator = userCreateValidator;
        _otpService = otpService;
        _webHostEnvironment = webHostEnvironment;
        _emailService = emailService;
        _verificationValidator = verificationValidator;
        _fileReadService = fileReadService;
        _studentUserCreateValidator = studentUserCreateValidator;
    }

    public async Task<string> CreateAccountAsync(UserPostDTO userPostDTO)
    {
        try
        {
            var validationResult = await _userCreateValidator.ValidateAsync(userPostDTO);
            if (!validationResult.IsValid) throw new RegisterFailException("Validation error(s) reported");

            AppUser user = await _userManager.FindByNameAsync(userPostDTO.UserName);
            if (user != null) throw new UserWithSameUsernameExists("User with same username already exists");

            user = await _userManager.FindByNameAsync(userPostDTO.Email);
            if (user != null) throw new UserWithSameEmailExists("User with same email already exists");


            //AppUser newUser = new AppUser();
            var newUser = _mapper.Map<AppUser>(userPostDTO);
            var otp = _otpService.GenerateOTP();
            newUser.OTP = otp;
            newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);

            var result = await _userManager.CreateAsync(newUser, userPostDTO.Password);
            if (!result.Succeeded)
            {
                throw new CreateUserFailException(result.Errors);
            }

            //string subject = "Verify Email";
            //string emailBody = await GetConfirmEmailTemplateAsync(userPostDTO.UserName, otp);
            //_emailService.Send(userPostDTO.Email, subject, emailBody);

            newUser.EmailConfirmed = true;

            foreach (var roleId in userPostDTO.RoleIds)
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

                if (role is not null)
                {
                    var resultRole = await _userManager.AddToRoleAsync(newUser, role.Name);
                }
            }

            var newUserId = newUser.Id;
            return newUserId;
        }
        catch (Exception)
        {

            throw;
        }
    }


    public async Task<string> CreateStudentAccountAsync(studentUserPostDTO studentUserPostDTO)
    {
        try
        {
            var validationResult = await _studentUserCreateValidator.ValidateAsync(studentUserPostDTO);
            if (!validationResult.IsValid) throw new RegisterFailException("Validation error(s) reported");

            AppUser user = await _userManager.FindByNameAsync(studentUserPostDTO.UserName);
            if (user != null) throw new UserWithSameUsernameExists("User with same username already exists");

            user = await _userManager.FindByNameAsync(studentUserPostDTO.Email);
            if (user != null) throw new UserWithSameEmailExists("User with same email already exists");


            //var teacherToUser = await _teacherRepository.GetSingleAsync(t => t.Id == userPostDTO.TeacherId);
            //var studentToUser = await _studentRepository.GetSingleAsync(s => s.Id == userPostDTO.StudentId);

            //if (teacherToUser is null && studentToUser is null)
            //{
            //    throw new UserEmptyEntityAssignmentException(" Wrong teacher/student assignment reported!There are no such entities existing!Try with another entityId!");
            //}

            //if (teacherToUser is not null && studentToUser is not null)
            //{
            //    throw new UserMultipleAssignmentTrialException("Multiple assignment reported");
            //}









            //AppUser newUser = new AppUser();
            var newUser = _mapper.Map<AppUser>(studentUserPostDTO);
            var otp = _otpService.GenerateOTP();
            newUser.OTP = otp;
            newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);

            var result = await _userManager.CreateAsync(newUser, studentUserPostDTO.Password);
            if (!result.Succeeded)
            {
                throw new CreateUserFailException(result.Errors);
            }

            string subject = "Verify Email";
            string emailBody = await GetConfirmEmailTemplateAsync(studentUserPostDTO.UserName, otp);
            _emailService.Send(studentUserPostDTO.Email, subject, emailBody);
            //if (teacherToUser is null && studentToUser is not null)
            //{


            //    // Generate OTP and set expiry date


            //    //newUser.IsActive = true;

            //    //newUser.Student = studentToUser;
            //    studentToUser.AppUserId = newUser.Id;
            //    studentToUser.AppUser = newUser;

            //}









            //AppUser newUser = new AppUser();
            //if (teacherToUser is null && studentToUser is not null)
            //{
            //    newUser = _mapper.Map<AppUser>(userPostDTO);


            //    // Generate OTP and set expiry date
            //    var otp = _otpService.GenerateOTP();
            //    newUser.OTP = otp;
            //    newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);


            //    string subject = "Verify Email";
            //    string emailBody = await GetConfirmEmailTemplateAsync(userPostDTO.UserName, otp);
            //    _emailService.Send(userPostDTO.Email, subject, emailBody);


            //    //newUser.IsActive = true;

            //    //newUser.Student = studentToUser;
            //    studentToUser.AppUserId = newUser.Id;
            //    studentToUser.AppUser = newUser;

            //    var result = await _userManager.CreateAsync(newUser, userPostDTO.Password);
            //    if (!result.Succeeded)
            //    {
            //        throw new CreateUserFailException(result.Errors);
            //    }
            //}









            //if (teacherToUser is not null && studentToUser is null)
            //{

            //    newUser = _mapper.Map<AppUser>(userPostDTO);


            //    // Generate OTP and set expiry date
            //    var otp = _otpService.GenerateOTP();
            //    newUser.OTP = otp;
            //    newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);


            //    string subject = "Verify Email";
            //    string emailBody = await GetConfirmEmailTemplateAsync(userPostDTO.UserName, otp);
            //    _emailService.Send(userPostDTO.Email, subject, emailBody);


            //    //newUser.IsActive = true;
            //    newUser.Teacher = teacherToUser;

            //    var result = await _userManager.CreateAsync(newUser, userPostDTO.Password);
            //    if (!result.Succeeded)
            //    {
            //        throw new CreateUserFailException(result.Errors);
            //    }
            //}

            await _userManager.AddToRoleAsync(newUser, Roles.Student.ToString());


            var newUserId = newUser.Id;
            return newUserId;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<string> ResendOTP(string userId)
    {
        // Use the userId from the request to find the user
        AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is null) throw new UserNotFoundByIdException($"User not found by Id: {userId}!");

        // Generate a new OTP and set expiry date
        var newOtp = _otpService.GenerateOTP();
        user.OTP = newOtp;
        user.OTPExpiryDate = DateTime.Now.AddMinutes(5);

        // Update the user with new OTP information
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new UpdateUserFailException(result.Errors);
        }

        // Email logic to resend OTP
        string subject = "Verify Email";
        string emailBody = await GetConfirmEmailTemplateAsync(user.UserName, newOtp);
        _emailService.Send(user.Email, subject, emailBody);

        return "OTP resent successfully. Check your Email please!";
    }

    public async Task<string> VerifyAccount(string userId, VerifyAccountDTO verifyAccountDTO)
    {
        var validationResult = await _verificationValidator.ValidateAsync(verifyAccountDTO);
        if (!validationResult.IsValid) throw new VerificationValidationFailException("Validation error(s) reported");

        // Use the userId from the request to find the user
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user is null) throw new UserNotFoundByIdException("User Id is not found!");

        // Check if the OTP matches and is not expired
        if (user.OTP != verifyAccountDTO.Otp || user.OTPExpiryDate <= DateTime.Now)
            throw new OTPValidateFailException("Invalid OTP or OTP has expired!");

        // Mark the email as confirmed
        user.EmailConfirmed = true;
        user.OTP = null;
        user.OTPExpiryDate = null;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new VerifyUserFailException(result.Errors);
        }

        return "Account verified successfully.";
    }



    //public async Task<string> CreateAccountAsync(UserPostDTO userPostDTO)
    //{
    //    try
    //    {
    //        var validationResult = await _userCreateValidator.ValidateAsync(userPostDTO);
    //        if (!validationResult.IsValid) throw new RegisterFailException("Validation error(s) reported");

    //        AppUser user = await _userManager.FindByNameAsync(userPostDTO.UserName);
    //        if (user != null) throw new UserWithSameUsernameExists("User with same username already exists");

    //        user = await _userManager.FindByNameAsync(userPostDTO.Email);
    //        if (user != null) throw new UserWithSameEmailExists("User with same email already exists");


    //        var teacherToUser = await _teacherRepository.GetSingleAsync(t => t.Id == userPostDTO.TeacherId);
    //        var studentToUser = await _studentRepository.GetSingleAsync(s => s.Id == userPostDTO.StudentId);

    //        if (teacherToUser is null && studentToUser is null)
    //        {
    //            throw new UserEmptyEntityAssignmentException(" Wrong teacher/student assignment reported!There are no such entities existing!Try with another entityId!");
    //        }

    //        if (teacherToUser is not null && studentToUser is not null)
    //        {
    //            throw new UserMultipleAssignmentTrialException("Multiple assignment reported");
    //        }


    //        AppUser newUser = new AppUser();
    //        if (teacherToUser is null && studentToUser is not null)
    //        {
    //            newUser = _mapper.Map<AppUser>(userPostDTO);


    //            // Generate OTP and set expiry date
    //            var otp = _otpService.GenerateOTP();
    //            newUser.OTP = otp;
    //            newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);


    //            string subject = "Verify Email";
    //            string emailBody = await GetConfirmEmailTemplateAsync(userPostDTO.UserName, otp);
    //            _emailService.Send(userPostDTO.Email, subject, emailBody);


    //            //newUser.IsActive = true;

    //            //newUser.Student = studentToUser;
    //            studentToUser.AppUserId = newUser.Id;
    //            studentToUser.AppUser = newUser;

    //            var result = await _userManager.CreateAsync(newUser, userPostDTO.Password);
    //            if (!result.Succeeded)
    //            {
    //                throw new CreateUserFailException(result.Errors);
    //            }
    //        }

    //        if (teacherToUser is not null && studentToUser is null)
    //        {

    //            newUser = _mapper.Map<AppUser>(userPostDTO);


    //            // Generate OTP and set expiry date
    //            var otp = _otpService.GenerateOTP();
    //            newUser.OTP = otp;
    //            newUser.OTPExpiryDate = DateTime.Now.AddMinutes(5);


    //            string subject = "Verify Email";
    //            string emailBody = await GetConfirmEmailTemplateAsync(userPostDTO.UserName, otp);
    //            _emailService.Send(userPostDTO.Email, subject, emailBody);


    //            //newUser.IsActive = true;
    //            newUser.Teacher = teacherToUser;

    //            var result = await _userManager.CreateAsync(newUser, userPostDTO.Password);
    //            if (!result.Succeeded)
    //            {
    //                throw new CreateUserFailException(result.Errors);
    //            }
    //        }


    //        foreach (var roleId in userPostDTO.RoleId)
    //        {
    //            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

    //            if (role is not null)
    //            {
    //                var resultRole = await _userManager.AddToRoleAsync(newUser, role.Name);
    //            }
    //        }


    //        var newUserId = newUser.Id;
    //        return newUserId;
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    public async Task DeleteUserAsync(string id)
    {
        try
        {
            var user = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new UserNotFoundByIdException("User Not found");

            if (user.Student is not null)
            {
                var student = await _studentRepository.GetSingleAsync(s => s.Id == user.Student.Id, isTracking: false);
                //var student = await _studentRepository.GetSingleAsync(s => s.Id == user.Student.Id, isTracking: false).AsNoTracking();
                if (student is null)
                {
                    throw new StudentNotFoundByIdException("Student not found");
                }

                // Detach the entity from the context
                _studentRepository.Detach(student);

                student.AppUser = null;

                _studentRepository.Update(student);
            }
            if (user.Teacher is not null)
            {
                //var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == user.Teacher.Id, isTracking: false);
                var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == user.Teacher.Id, isTracking: false);
                if (teacher is null)
                {
                    throw new TeacherNotFoundByIdException("Teacher not found");
                }

                // Detach the entity from the context
                _teacherRepository.Detach(teacher);

                teacher.AppUser = null;

                _teacherRepository.Update(teacher);
            }

            var identityResult = await _userManager.DeleteAsync(user);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<UserGetDTO>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).ToListAsync();
            var userGetDTOs = new List<UserGetDTO>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userRole = new UserGetDTO()
                {
                    Roles = roles.ToList(),
                };
                userRole = _mapper.Map(user, userRole);
                userGetDTOs.Add(userRole);

            }

            return userGetDTOs;
        }
        catch (Exception)
        {

            throw;
        }
    }

    //public Task<List<UnassignedUserGetDTO>> GetAllUnassignedUsersAsync()
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<List<UnassignedUserGetDTO>> GetAllUnassignedUsersAsync()
    {
        try
        {
            //var unassignedUsers = await _userManager.Users
            //    .Where(u => u.Student == null && u.Teacher == null)
            //    .Where(u => !_userManager.IsInRoleAsync(u, "Admin").Result && !_userManager.IsInRoleAsync(u, "Moderator").Result) // Users not in Admin or Moderator role
            //    .ToListAsync();
            // Fetch users without Student or Teacher from the database
            var unassignedUsers = await _userManager.Users
                .Where(u => u.Student == null && u.Teacher == null)
                .ToListAsync();

            // Filter out users in Admin or Moderator role locally (client-side)
            unassignedUsers = unassignedUsers
                .Where(u => !_userManager.IsInRoleAsync(u, "Admin").Result && !_userManager.IsInRoleAsync(u, "Moderator").Result)
                .ToList();

            var unassignedUserDTOs = new List<UnassignedUserGetDTO>();

            foreach (var user in unassignedUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var unassignedUserDTO = new UnassignedUserGetDTO()
                {
                    Roles = roles.ToList(),
                };

                // Assuming UnassignedUserGetDTO has appropriate mapping logic
                unassignedUserDTO = _mapper.Map(user, unassignedUserDTO);
                unassignedUserDTOs.Add(unassignedUserDTO);
            }

            return unassignedUserDTOs;
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<UserGetDTO> GetUserByIdAsync(string Id)
    {
        var user = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Id == Id);

        if (user is null)
        {
            throw new UserNotFoundByIdException("User not found");
        }

        var userGetDTO = _mapper.Map<UserGetDTO>(user);

        var roles = await _userManager.GetRolesAsync(user);


        userGetDTO.Roles = roles.ToList();

        return userGetDTO;
    }

    public Task<UserPutDTO> GetUserByIdForUpdateAsync(string Id)
    {
        throw new NotImplementedException();
    }

    //public async Task UpdateUserAsync(string Id, UserPutDTO userPutDTO)
    //{
    //    //var user = await _context.Users.Include(u=>u.Student).FirstOrDefaultAsync(u => u.Id == Id);
    //    var user = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Id == Id);

    //    if (user is null)
    //    {
    //        throw new UserNotFoundByIdException("User not found");
    //    }





    //    var teacherToUser = await _teacherRepository.GetSingleAsync(t => t.Id == userPutDTO.TeacherId);
    //    var studentToUser = await _studentRepository.GetSingleAsync(s => s.Id == userPutDTO.StudentId);


    //    if (teacherToUser is null && studentToUser is null)
    //    {
    //        throw new UserEmptyEntityAssignmentException(" Wrong teacher/student assignment reported!There are no such entities existing!Try with another entityId!");
    //    }

    //    if (teacherToUser is not null && studentToUser is not null)
    //    {
    //        throw new UserMultipleAssignmentTrialException("Multiple assignment reported");
    //    }






    //    //if (userPutDTO.TeacherId is not null && userPutDTO.StudentId is not null)
    //    //{
    //    //    throw new UserDTOTeacherAndStudentException("Student and teacher cant be assigned at the same time");
    //    //}
    //    user = _mapper.Map(userPutDTO, user);


    //    if (userPutDTO.RoleId?.Count() > 0)
    //    {
    //        List<string>? newRoles = new List<string>();
    //        foreach (var roleId in userPutDTO.RoleId)
    //        {
    //            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
    //            if (role is null)
    //            {
    //                throw new RoleNotFoundByIdException($"Role with Id:{roleId} doesn't exist");
    //            }
    //            newRoles.Add(role.Name);
    //        }

    //        var roles = await _userManager.GetRolesAsync(user);

    //        var removeRoles = roles.Except(newRoles);
    //        await _userManager.RemoveFromRolesAsync(user, removeRoles);

    //        var rolesToAdd = newRoles.Except(roles).ToList();
    //        await _userManager.AddToRolesAsync(user, rolesToAdd);


    //    }
    //    Student? student = user.Student;
    //    if (userPutDTO.StudentId is not null)
    //    {
    //        var existingStudent = await _studentRepository.GetSingleAsync(s => s.Id == userPutDTO.StudentId, isTracking: true, "AppUser");
    //        if (existingStudent is null)
    //        {
    //            throw new StudentNotFoundByIdException("Student not found");
    //        }
    //        if (existingStudent.AppUser is not null && existingStudent.AppUser.Id != user.Id)
    //        {
    //            throw new StudentAlreadyHasAccountException("Student already has account");
    //        }

    //        user.Student = existingStudent;
    //        existingStudent.AppUser = user;
    //        _studentRepository.Update(existingStudent);

    //    }
    //    else
    //    {
    //        if (student?.AppUser is not null)
    //        {
    //            student.AppUser = null;
    //            user.Student = null;
    //            _studentRepository.Update(student);

    //        }

    //    }


    //    Teacher? teacher = user.Teacher;
    //    if (userPutDTO.TeacherId is not null)
    //    {
    //        var existingTeacher = await _teacherRepository.GetSingleAsync(t => t.Id == userPutDTO.TeacherId, isTracking: true, "AppUser");
    //        if (existingTeacher is null)
    //        {
    //            throw new TeacherNotFoundByIdException("Teacher not Found");
    //        }
    //        if (existingTeacher.AppUser is not null && existingTeacher.AppUser.Id != user.Id)
    //        {
    //            throw new TeacherAlreadyHasAccountException("Teacher already has account");
    //        }

    //        existingTeacher.AppUser = user;
    //        user.Teacher = existingTeacher;
    //        _teacherRepository.Update(existingTeacher);
    //    }
    //    else
    //    {
    //        if (teacher?.AppUser is not null)
    //        {
    //            teacher.AppUserId = null;
    //            user.Teacher = null;
    //            _teacherRepository.Update(teacher);

    //        }
    //    }

    //    var result = await _userManager.UpdateAsync(user);
    //    if (!result.Succeeded)
    //    {
    //        throw new UpdateUserFailException(result.Errors);
    //    }

    //    await _context.SaveChangesAsync();


    //}


    public async Task UpdateUserAsync(string Id, UserPutDTO userPutDTO)
    {

        //var teacherToUser = await _teacherRepository.GetSingleAsync(t => t.Id == userPutDTO.TeacherId);
        //var studentToUser = await _studentRepository.GetSingleAsync(s => s.Id == userPutDTO.StudentId);


        //if (teacherToUser is null && studentToUser is null)
        //{
        //    throw new UserEmptyEntityAssignmentException(" Wrong teacher/student assignment reported!There are no such entities existing!Try with another entityId!");
        //}

        //if (teacherToUser is not null && studentToUser is not null)
        //{
        //    throw new UserMultipleAssignmentTrialException("Multiple assignment reported");
        //}






        //if (userPutDTO.TeacherId is not null && userPutDTO.StudentId is not null)
        //{
        //    throw new UserDTOTeacherAndStudentException("Student and teacher cant be assigned at the same time");
        //}

        //var user = await _context.Users.Include(u=>u.Student).FirstOrDefaultAsync(u => u.Id == Id);
        var user = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Id == Id);

        if (user is null)
        {
            throw new UserNotFoundByIdException("User not found");
        }




        user = _mapper.Map(userPutDTO, user);


        if (userPutDTO.RoleIds?.Count() > 0)
        {
            List<string>? newRoles = new List<string>();
            foreach (var roleId in userPutDTO.RoleIds)
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                if (role is null)
                {
                    throw new RoleNotFoundByIdException($"Role with Id:{roleId} doesn't exist");
                }
                newRoles.Add(role.Name);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var removeRoles = roles.Except(newRoles);
            await _userManager.RemoveFromRolesAsync(user, removeRoles);

            var rolesToAdd = newRoles.Except(roles).ToList();
            await _userManager.AddToRolesAsync(user, rolesToAdd);


        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new UpdateUserFailException(result.Errors);
        }

        await _context.SaveChangesAsync();


    }


    private async Task<string> GetConfirmEmailTemplateAsync(string username, string otp)
    {
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "templates", "otp-verification.html");

        string body = string.Empty;
        body = _fileReadService.ReadFile(path, body);

        //using StreamReader streamReader = new StreamReader(path);
        //string result = await streamReader.ReadToEndAsync();
        body = body.Replace("{{username}}", username);
        body = body.Replace("{{otp}}", otp);
        return body;
    }
}