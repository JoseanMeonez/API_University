using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
	public class ScheduleRepository : IScheduleRepository
	{
		private readonly ApiUniversityContext _apiUniversityContext;

		public ScheduleRepository(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}

		public async Task<Schedule> Create(Schedule schedule)
		{
			var res = _apiUniversityContext.Add(schedule);
			await _apiUniversityContext.SaveChangesAsync();

			return res.Entity;
		}

		public Task<Schedule> Get(int id)
		{
			return _apiUniversityContext.Schedules.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<IEnumerable<Schedule>> GetAll()
		{
			return await _apiUniversityContext.Schedules.ToListAsync();
		}

		public async Task<Schedule> Update(Schedule schedule)
		{
			var res = _apiUniversityContext.Schedules.Update(schedule);
			await _apiUniversityContext.SaveChangesAsync();
			return res.Entity;
		}

		public List<Schedule> ProfessorHourVerifier(Schedule schedule)
		{
			var res = _apiUniversityContext.Schedules
				.Where(x => schedule.StartHour == x.StartHour && schedule.EndHour == x.EndHour && schedule.ProfessorId == x.ProfessorId)
				.ToList();

      return res;
		}

		public List<Schedule> ClassroomHourVerifier(Schedule schedule)
		{
			var res = _apiUniversityContext.Schedules
				.Where(x => schedule.StartHour == x.StartHour && schedule.EndHour == x.EndHour && schedule.ClassroomId == x.ClassroomId)
				.ToList();

			return res;
		}
	}
}
