using API_University.Data;

namespace API_University.Repositories
{
	public interface IScheduleRepository
	{
		Task<Schedule> Get(int id);

		Task<IEnumerable<Schedule>> GetAll();

		Task<Schedule> Create(Schedule schedule);

		Task<Schedule> Update(Schedule schedule);

		List<Schedule> ProfessorHourVerifier(Schedule schedule);

		List<Schedule> ClassroomHourVerifier(Schedule schedule);
	}
}
