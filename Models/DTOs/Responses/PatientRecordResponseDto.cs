namespace PatientManagement.Models.DTOs.Responses;

public class PatientRecordResponseDto
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string Symptoms { get; set; }

    public string Diagnosis { get; set; }

    public string Treatment { get; set; }

    public string Notes { get; set; }
    
    public DateTime RecordDate { get; set; }
    
    public Patient Patient { get; set; }
}