using FluentValidation;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;

namespace StudentManagementSystemUNEC.Business.Validators.UserValidators;

public class StudentUserPostDTOValidator : AbstractValidator<studentUserPostDTO>
{
    public StudentUserPostDTOValidator()
    {
        RuleFor(register => register.UserName)
           .NotEmpty()
           .WithMessage("Username is required")
           .MaximumLength(100)
           .WithMessage("Username cannot be more than 100 characters");

        RuleFor(register => register.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid Email Address");

        RuleFor(register => register.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

        RuleFor(register => register.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required")
                .Equal(register => register.Password)
                .WithMessage("Password and Confirm Password must match");
    }
}