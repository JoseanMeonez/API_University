namespace API_University.Data;

public partial class Classroom
{
    public int Id { get; set; }

		public string? Code { get; set; }

		public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
