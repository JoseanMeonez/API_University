using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
	public class ClassroomRepository : IClassroomRepository
	{
		private readonly ApiUniversityContext _apiUniversityContext;

		public ClassroomRepository(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}
		public async Task<Classroom> Create(Classroom classrooms)
		{
			var res = _apiUniversityContext.Add(classrooms);
			await _apiUniversityContext.SaveChangesAsync();

			return res.Entity;
		}

		public Task<Classroom> Get(int id)
		{
			return _apiUniversityContext.Classrooms.FirstOrDefaultAsync(a => a.Id == id);
		}

		public int GetLastId()
		{
			var lastId = _apiUniversityContext.Classrooms.OrderByDescending(x => x.Id).FirstOrDefault();

			if (lastId != null)
			{
				return lastId.Id;
			}

			return 0;
		}

		public async Task<IEnumerable<Classroom>> GetAll()
		{
			return await _apiUniversityContext.Classrooms.ToListAsync();
		}

		public async Task<Classroom> Update(Classroom classrooms)
		{
			var res = _apiUniversityContext.Classrooms.Update(classrooms);
			await _apiUniversityContext.SaveChangesAsync();
			return res.Entity;
		}
	}
}
