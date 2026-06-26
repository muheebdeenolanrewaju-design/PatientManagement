using Microsoft.AspNetCore.Mvc;
using PatientManagement.Models.DTOs.Requests;
using PatientManagement.Repositories.Interfaces;

namespace PatientManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("GetAllPatients")]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await _patientRepository.GetAllPatients();
        return Ok(patients);
    }

    [HttpGet("GetPatientById/{id}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        var patient = await _patientRepository.GetPatientById(id);
        return Ok(patient);
    }

    [HttpPost("CreatePatient")]
    public async Task<IActionResult> CreatePatient(CreatePatientDto dto)
    {
        var patient = await _patientRepository.CreatePatient(dto);
        return Ok(patient);
    }

    [HttpPut("UpdatePatient/{id}")]
    public async Task<IActionResult> UpdatePatient(int id, UpdatePatientDto dto)
    {
        var patient = await _patientRepository.UpdatePatient(id, dto);
        return Ok(patient);
    }

    [HttpDelete("DeletePatient/{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var deleted = await _patientRepository.DeletePatient(id);
        return Ok(deleted);
    }
    
    [HttpPut("RestorePatient/{id}")]
    public async Task<IActionResult> RestorePatient(int id)
    {
        var restored = await _patientRepository.RestorePatient(id);
        return Ok(restored);
    }
}