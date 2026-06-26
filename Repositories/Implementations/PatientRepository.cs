using Microsoft.EntityFrameworkCore;
using PatientManagement.Auth;
using PatientManagement.Data;
using PatientManagement.Models;
using PatientManagement.Models.DTOs.Requests;
using PatientManagement.Models.DTOs.Responses;
using PatientManagement.Repositories.Interfaces;

namespace PatientManagement.Repositories.Implementations;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL PATIENTS
    public async Task<IEnumerable<PatientResponseDto>> GetAllPatients()
    {
        var patients = await _context.Patients
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        return patients.Select(MapToDto);
    }

    // GET PATIENT BY ID
    public async Task<PatientResponseDto> GetPatientById(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (patient == null)
            throw new Exception("Patient not found.");

        return MapToDto(patient);
    }

    // CREATE PATIENT
    public async Task<PatientResponseDto> CreatePatient(CreatePatientDto dto)
    {
        var emailExists = await _context.Patients
            .AnyAsync(x => x.Email == dto.Email && !x.IsDeleted);

        if (emailExists)
            throw new Exception("Email already exists.");
        
        int totalPatients = await _context.Patients.CountAsync();

        string patientNumber =
            PatientNumberGenerator.GeneratePatientNumber(totalPatients + 1);

        var patient = new Patient
        {
            PatientNumber = patientNumber,
            FullName = dto.FullName,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            DateRegistered = DateTime.UtcNow,
            IsDeleted = false
        };

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        return MapToDto(patient);
    }

    // UPDATE PATIENT
    public async Task<PatientResponseDto> UpdatePatient(int id, UpdatePatientDto dto)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (patient == null)
            throw new Exception("Patient not found.");

        var emailExists = await _context.Patients
            .AnyAsync(x => x.Email == dto.Email &&
                           x.Id != id &&
                           !x.IsDeleted);

        if (emailExists)
            throw new Exception("Email already exists.");

        patient.FullName = dto.FullName;
        patient.Email = dto.Email;
        patient.DateOfBirth = dto.DateOfBirth;

        await _context.SaveChangesAsync();

        return MapToDto(patient);
    }

    // SOFT DELETE
    public async Task<bool> DeletePatient(int id)
    {
        var patient = await _context.Patients
            .Include(x => x.PatientRecords)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (patient == null)
            throw new Exception("Patient not found.");

        patient.IsDeleted = true;

        foreach (var record in patient.PatientRecords)
        {
            record.IsDeleted = true;
        }

        await _context.SaveChangesAsync();

        return true;
    }
    
    // Soft Restore
    public async Task<bool> RestorePatient(int id)
    {
        var patient = await _context.Patients
            .Include(x => x.PatientRecords)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (patient == null)
            throw new Exception("Patient not found.");

        if (!patient.IsDeleted)
            throw new Exception("Patient is already active.");

        // Restore patient
        patient.IsDeleted = false;

        // Restore all associated records
        foreach (var record in patient.PatientRecords)
        {
            record.IsDeleted = false;
        }

        await _context.SaveChangesAsync();

        return true;
    }

    // MAPPING
    private static PatientResponseDto MapToDto(Patient patient)
    {
        return new PatientResponseDto
        {
            Id = patient.Id,
            PatientNumber = patient.PatientNumber,
            FullName = patient.FullName,
            Email = patient.Email,
            DateOfBirth = patient.DateOfBirth,
            DateRegistered = patient.DateRegistered
        };
    }
}