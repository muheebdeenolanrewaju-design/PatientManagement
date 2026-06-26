namespace PatientManagement.Auth;

public  static class PatientNumberGenerator
{
    public static string GeneratePatientNumber(int nextNumber)
    {
        return $"PAT-{nextNumber:D4}";
    }
}