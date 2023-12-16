using FluentValidation;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;

namespace StudentManagementSystemUNEC.Business.Validators.AccountValidators;

public class VerifyAccountDTOValidator : AbstractValidator<VerifyAccountDTO>
{
    public VerifyAccountDTOValidator()
    {
        RuleFor(verifyAccount => verifyAccount.Otp)
             .NotEmpty()
             .WithMessage("Otp is required");
    }
}