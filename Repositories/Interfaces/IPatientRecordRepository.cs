using PatientManagement.Models.DTOs.Requests;
using PatientManagement.Models.DTOs.Responses;

namespace PatientManagement.Repositories.Interfaces;

public interface IPatientRecordRepository
{
    Task<IEnumerable<PatientRecordResponseDto>> GetAllPatientRecords();
    
    Task<PatientRecordResponseDto> GetPatientRecordById(int Id);

    Task<PatientRecordResponseDto> CreatePatientRecord(CreatePatientRecordDto dto);

    Task<PatientRecordResponseDto> UpdatePatientRecord(int Id, UpdatePatientRecordDto dto);
    
    Task<bool> DeletePatientRecord(int Id);
    
    Task<bool> RestorePatientRecord(int id);
}