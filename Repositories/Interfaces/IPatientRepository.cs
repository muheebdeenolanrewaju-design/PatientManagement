using PatientManagement.Models.DTOs.Requests;
using PatientManagement.Models.DTOs.Responses;

namespace PatientManagement.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<PatientResponseDto>> GetAllPatients();
    
    Task<PatientResponseDto> GetPatientById(int Id);

    Task<PatientResponseDto> CreatePatient(CreatePatientDto dto);

    Task<PatientResponseDto> UpdatePatient(int Id, UpdatePatientDto dto);
    
    Task<bool> DeletePatient(int Id);

    Task<bool> RestorePatient(int id);

}