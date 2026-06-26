using System.Data;
using FluentValidation;
using PatientManagement.Models.DTOs.Requests;

namespace PatientManagement.Validator;

public class CreatePatientRecordDtoValidator : AbstractValidator<CreatePatientRecordDto>
{
    public CreatePatientRecordDtoValidator()
    {
        RuleFor(x => x.Symptoms)
            .NotEmpty()
            .WithMessage("Please specify at least one symptom");
        
        RuleFor(x => x.Diagnosis)
            .NotEmpty()
            .WithMessage("Please specify at least one diagnosis");
        
        RuleFor(x => x.Treatment)
            .NotEmpty()
            .WithMessage("Please specify at least one treatment");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters.");
    }
}
