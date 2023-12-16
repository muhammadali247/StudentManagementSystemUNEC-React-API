using FluentValidation;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

namespace StudentManagementSystemUNEC.Business.Validators.StudentValidators;

public class StudentPostDTOValidator : AbstractValidator<StudentPostDTO>
{
	public StudentPostDTOValidator()
	{
		//RuleFor(x => x.Image)
		//	.NotNull().WithMessage("Image URL is required!")
		//	.NotEmpty().WithMessage("Image URL is required!")
  //          .MaximumLength(255).WithMessage("Image URL can't be longer than 255 characters!");

   //     RuleFor(x => x.Email)
			//.NotNull().WithMessage("Student Email is required!")
   //         .NotEmpty().WithMessage("Student Email is required!")
   //         .MaximumLength(255).WithMessage("Email length can't be longer than 255 characters!");

   //     RuleFor(x => x.Username)
			//.NotNull().WithMessage("Student username is required!")
   //         .NotEmpty().WithMessage("Student username is required!")
   //         .MaximumLength(255).WithMessage("Image URL can't be longer than 255 characters!");

   //     RuleFor(x => x.Password)
			//.NotNull().WithMessage("Password is required!")
   //         .NotEmpty().WithMessage("Password is required!")
   //         .MaximumLength(255).WithMessage("Password can't be longer than 255 characters!");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Student name is required!")
            .NotEmpty().WithMessage("Student name is required!")
            .MaximumLength(255).WithMessage("Student name can't be longer than 255 characters!");

        RuleFor(x => x.Surname)
            .NotNull().WithMessage("Student surname is required!")
            .NotEmpty().WithMessage("Student surname is required!")
            .MaximumLength(255).WithMessage("Student surname can't be longer than 255 characters!");

        RuleFor(x => x.middleName)
            .NotNull().WithMessage("Student middle name is required!")
            .NotEmpty().WithMessage("Student middle name is required!")
            .MaximumLength(255).WithMessage("Student middle name can't be longer than 255 characters!");

        RuleFor(x => x.admissionYear)
            .NotEmpty().WithMessage("Student admission year must be provided!")
            .NotNull();

        RuleFor(x => x.educationStatus)
           .NotEmpty().WithMessage("Student education status must be provided!")
           .NotNull();

        RuleFor(x => x.BirthDate)
           .NotEmpty().WithMessage("Student birth date can't be omitted empty!");

        RuleFor(x => x.corporativeEmail)
            .NotEmpty().WithMessage("Student corporative email can't be omitted empty!")
            .MaximumLength(255).WithMessage("Email length can't be longer than 255 characters!");

        RuleFor(x => x.corporativePassword)
            .NotEmpty().WithMessage("Corporative password can't be omitted empty!")
            .MaximumLength(255).WithMessage("Password can't be longer than 255 characters!");

        RuleFor(x => x.Gender)
           .NotNull().WithMessage("Student gender must be provided")
           .NotEmpty().WithMessage("Student gender can't be omitted empty")
           .NotNull();

        RuleFor(x => x.Country)
           .NotNull().WithMessage("Student country must be provided")
           .NotEmpty().WithMessage("Student country can't be omitted left!")
           .NotNull();
    }
}
