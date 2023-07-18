using System;
using System.Collections.Generic;

namespace API_University.Data;

public partial class Professor
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

	  public string CelNumber { get; set; } = null!;

	  public string Email { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<ProfessorClass> ProfessorClasses { get; set; } = new List<ProfessorClass>();

	  public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
