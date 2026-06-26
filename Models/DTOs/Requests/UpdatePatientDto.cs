namespace PatientManagement.Models.DTOs.Requests;

public class UpdatePatientDto
{
    public string FullName { get; set; }
   
    public string Email { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
}