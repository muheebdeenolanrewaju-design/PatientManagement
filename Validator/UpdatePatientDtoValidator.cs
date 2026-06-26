using FluentValidation;
using PatientManagement.Models.DTOs.Requests;

namespace PatientManagement.Validator;

public class UpdatePatientDtoValidator : AbstractValidator<UpdatePatientDto>
{
    public UpdatePatientDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MinimumLength(3)
            .WithMessage("Full name must be at least 3 characters.")
            .MaximumLength(50)
            .WithMessage("Full name must not be more than 50 characters.");
        

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of Birth is required")
            .LessThan(DateTime.Today)
            .WithMessage("Date of birth must be in the past.");

    }
}