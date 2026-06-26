using Microsoft.AspNetCore.Mvc;
using PatientManagement.Models.DTOs.Requests;
using PatientManagement.Repositories.Interfaces;

namespace PatientManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientRecordController : ControllerBase
{
    private readonly IPatientRecordRepository _patientRecordRepository;

    public PatientRecordController(IPatientRecordRepository patientRecordRepository)
    {
        _patientRecordRepository = patientRecordRepository;
    }

    [HttpGet("GetAllPatientRecords")]
    public async Task<IActionResult> GetAllPatientRecords()
    {
        var records = await _patientRecordRepository.GetAllPatientRecords();
        return Ok(records);
    }

    [HttpGet("GetPatientRecordById/{id}")]
    public async Task<IActionResult> GetPatientRecordById(int id)
    {
        var record = await _patientRecordRepository.GetPatientRecordById(id);
        return Ok(record);
    }

    [HttpPost("CreatePatientRecord")]
    public async Task<IActionResult> CreatePatientRecord(CreatePatientRecordDto dto)
    {
        var record = await _patientRecordRepository.CreatePatientRecord(dto);
        return Ok(record);
    }

    [HttpPut("UpdatePatientRecord/{id}")]
    public async Task<IActionResult> UpdatePatientRecord(int id, UpdatePatientRecordDto dto)
    {
        var record = await _patientRecordRepository.UpdatePatientRecord(id, dto);
        return Ok(record);
    }

    [HttpDelete("DeletePatientRecord/{id}")]
    public async Task<IActionResult> DeletePatientRecord(int id)
    {
        var deleted = await _patientRecordRepository.DeletePatientRecord(id);
        return Ok(deleted);
    }

    [HttpPut("RestorePatientRecord/{id}")]
    public async Task<IActionResult> RestorePatientRecord(int id)
    {
        var restored = await _patientRecordRepository.RestorePatientRecord(id);
        return Ok(restored);
    }
}