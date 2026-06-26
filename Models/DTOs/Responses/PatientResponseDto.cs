namespace PatientManagement.Models.DTOs.Responses;

public class PatientResponseDto
{
    public int Id { get; set; }
   
    public string PatientNumber { get; set; }
    
    public string FullName { get; set; }
   
    public string Email { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public DateTime DateRegistered { get; set; }
}