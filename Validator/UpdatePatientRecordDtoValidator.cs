using FluentValidation;
using PatientManagement.Models.DTOs.Requests;

namespace PatientManagement.Validator;

public class UpdatePatientRecordDtoValidator: AbstractValidator<UpdatePatientRecordDto>
{
    public UpdatePatientRecordDtoValidator()
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