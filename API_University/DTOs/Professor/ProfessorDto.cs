namespace API_University.DTOs.Professor
{
  public class ProfessorDto
  {
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CelNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }
  }
}
