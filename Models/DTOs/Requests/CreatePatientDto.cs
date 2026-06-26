namespace PatientManagement.Models.DTOs.Requests;

public class CreatePatientDto
{
     public string FullName { get; set; }
       
     public string Email { get; set; }
        
     public DateTime DateOfBirth { get; set; }
        
     public DateTime DateRegistered { get; set; }
}