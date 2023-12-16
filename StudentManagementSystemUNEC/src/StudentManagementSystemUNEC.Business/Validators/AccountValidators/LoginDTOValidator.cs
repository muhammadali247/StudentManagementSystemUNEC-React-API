using FluentValidation;
using StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;

namespace StudentManagementSystemUNEC.Business.Validators.AccountValidators;

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(login => login.UsernameOrEmail)
               .NotEmpty()
               .WithMessage("Username or Email is required")
               .MaximumLength(100)
               .WithMessage("Username or Email cannot be more than 100 characters");

        RuleFor(login => login.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}