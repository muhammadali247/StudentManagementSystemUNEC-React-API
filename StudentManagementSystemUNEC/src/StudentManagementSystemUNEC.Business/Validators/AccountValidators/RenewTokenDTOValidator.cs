using FluentValidation;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;

namespace StudentManagementSystemUNEC.Business.Validators.AccountValidators;

public class RenewTokenDTOValidator : AbstractValidator<RenewTokenDTO>
{
    public RenewTokenDTOValidator()
    {
        RuleFor(renewToken => renewToken.Token)
          .NotEmpty()
          .WithMessage("Token is required");
    }
}