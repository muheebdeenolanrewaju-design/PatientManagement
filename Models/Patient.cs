namespace PatientManagement.Models;

public class Patient
{
   public int Id { get; set; }
   
   public string PatientNumber { get; set; }
    
    public string FullName { get; set; }
   
    public string Email { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public DateTime DateRegistered { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public ICollection<PatientRecord> PatientRecords { get; set; }
}