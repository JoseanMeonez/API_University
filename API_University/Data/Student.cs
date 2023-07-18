using System;
using System.Collections.Generic;

namespace API_University.Data;

public partial class Student
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string CelNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<StudentRegistration>? StudentRegistrations { get; set; } = new List<StudentRegistration>();

	  public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
