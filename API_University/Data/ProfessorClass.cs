using System;
using System.Collections.Generic;

namespace API_University.Data;

public partial class ProfessorClass
{
    public int Id { get; set; }

    public int ProfessorId { get; set; }

    public int ClassId { get; set; }

    public virtual Class? Class { get; set; } = null!;

    public virtual Professor? Professor { get; set; } = null!;
}
