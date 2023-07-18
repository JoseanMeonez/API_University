using System;
using System.Collections.Generic;

namespace API_University.Data;

public partial class Class
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProfessorClass> ProfessorClasses { get; set; } = new List<ProfessorClass>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; } = new List<StudentRegistration>();
}
