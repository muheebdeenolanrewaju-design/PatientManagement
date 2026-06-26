namespace PatientManagement.Models.DTOs.Requests;

public class UpdatePatientRecordDto
{
    public string Symptoms { get; set; }

    public string Diagnosis { get; set; }

    public string Treatment { get; set; }

    public string Notes { get; set; }
}