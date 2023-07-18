using System;
using System.Collections.Generic;

namespace API_University.Data;

public partial class Schedule
{
    public int Id { get; set; }

    public DateTime StartHour { get; set; }

    public DateTime EndHour { get; set; }

    public int ClassId { get; set; }

    public int ClassroomId { get; set; }

    public int StudentId { get; set; }

	  public int ProfessorId { get; set; }

	  public virtual Class? Class { get; set; } = null!;

    public virtual Classroom? Classroom { get; set; } = null!;

	  public virtual Student? Student { get; set; } = null!;

	  public virtual Professor? Professor { get; set; } = null!;
}
