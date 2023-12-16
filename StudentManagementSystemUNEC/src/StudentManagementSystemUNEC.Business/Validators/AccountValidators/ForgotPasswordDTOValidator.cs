using FluentValidation;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;

namespace StudentManagementSystemUNEC.Business.Validators.AccountValidators;

public class ForgotPasswordDTOValidator : AbstractValidator<ForgotPasswordDTO>
{
    public ForgotPasswordDTOValidator()
    {
        RuleFor(forgotPassword => forgotPassword.Email)
           .NotEmpty()
           .WithMessage("Email is required")
           .EmailAddress()
           .WithMessage("Invalid Email Address");
    }
}