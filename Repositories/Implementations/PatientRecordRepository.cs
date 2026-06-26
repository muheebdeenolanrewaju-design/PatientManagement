using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Models;
using PatientManagement.Models.DTOs.Requests;
using PatientManagement.Models.DTOs.Responses;
using PatientManagement.Repositories.Interfaces;

namespace PatientManagement.Repositories.Implementations;

public class PatientRecordRepository : IPatientRecordRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL PATIENT RECORDS
    public async Task<IEnumerable<PatientRecordResponseDto>> GetAllPatientRecords()
    {
        var records = await _context.PatientRecords
            .Include(x => x.Patient)
            .Where(x => !x.IsDeleted && !x.Patient.IsDeleted)
            .ToListAsync();

        return records.Select(MapToDto);
    }

    // GET RECORD BY ID
    public async Task<PatientRecordResponseDto> GetPatientRecordById(int id)
    {
        var record = await _context.PatientRecords
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id &&
                                      !x.IsDeleted &&
                                      !x.Patient.IsDeleted);

        if (record == null)
            throw new Exception("Patient record not found.");

        return MapToDto(record);
    }

    // CREATE PATIENT RECORD
    public async Task<PatientRecordResponseDto> CreatePatientRecord(CreatePatientRecordDto dto)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.Id == dto.PatientId && !x.IsDeleted);

        if (patient == null)
            throw new Exception("Patient not found.");

        var record = new PatientRecord
        {
            PatientId = dto.PatientId,
            Symptoms = dto.Symptoms,
            Diagnosis = dto.Diagnosis,
            Treatment = dto.Treatment,
            Notes = dto.Notes,
            RecordDate = DateTime.UtcNow,
            IsDeleted = false
        };

        await _context.PatientRecords.AddAsync(record);
        await _context.SaveChangesAsync();

        return await GetPatientRecordById(record.Id);
    }

    // UPDATE PATIENT RECORD
    public async Task<PatientRecordResponseDto> UpdatePatientRecord(int id, UpdatePatientRecordDto dto)
    {
        var record = await _context.PatientRecords
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id &&
                                      !x.IsDeleted &&
                                      !x.Patient.IsDeleted);

        if (record == null)
            throw new Exception("Patient record not found.");

        record.Symptoms = dto.Symptoms;
        record.Diagnosis = dto.Diagnosis;
        record.Treatment = dto.Treatment;
        record.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return await GetPatientRecordById(id);
    }

    // SOFT DELETE RECORD
    public async Task<bool> DeletePatientRecord(int id)
    {
        var record = await _context.PatientRecords
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (record == null)
            throw new Exception("Patient record not found.");

        record.IsDeleted = true;

        await _context.SaveChangesAsync();

        return true;
    }
    
    //Soft Restore
    public async Task<bool> RestorePatientRecord(int id)
    {
        var record = await _context.PatientRecords
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (record == null)
            throw new Exception("Patient record not found.");

        if (!record.IsDeleted)
            throw new Exception("Patient record is already active.");

        if (record.Patient.IsDeleted)
            throw new Exception("Restore the patient first.");

        record.IsDeleted = false;

        await _context.SaveChangesAsync();

        return true;
    }

    // DTO MAPPING
    private static PatientRecordResponseDto MapToDto(PatientRecord record)
    {
        return new PatientRecordResponseDto
        {
            Id = record.Id,
            PatientId = record.PatientId,
            Symptoms = record.Symptoms,
            Diagnosis = record.Diagnosis,
            Treatment = record.Treatment,
            Notes = record.Notes,
            RecordDate = record.RecordDate,

            PatientNumber = record.Patient.PatientNumber,
            PatientName = record.Patient.FullName
        };
    
    }
}