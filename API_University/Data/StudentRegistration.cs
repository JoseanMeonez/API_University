using System;
using System.Collections.Generic;

namespace API_University.Data;

public partial class StudentRegistration
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public virtual Class? Class { get; set; } = null!;

    public virtual Student? Student { get; set; } = null!;
}
