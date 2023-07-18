using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
	public class ProfessorClassRepository : IProfessorClassRepository
	{
		private readonly ApiUniversityContext _apiUniversityContext;

		public ProfessorClassRepository(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}

		public async Task<ProfessorClass> Create(ProfessorClass professorClasses)
		{
			var res = _apiUniversityContext.Add(professorClasses);
			await _apiUniversityContext.SaveChangesAsync();

			return res.Entity;
		}

		public Task<ProfessorClass> Get(int id)
		{
			return _apiUniversityContext.ProfessorClasses.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<IEnumerable<ProfessorClass>> GetAll()
		{
			return await _apiUniversityContext.ProfessorClasses.ToListAsync();
		}

		public int CountById(int id)
		{
			int count = _apiUniversityContext.ProfessorClasses.Count(x => x.ProfessorId == id);
			return count;
		}

		public async Task<ProfessorClass> Update(ProfessorClass professorClasses)
		{
			var res = _apiUniversityContext.ProfessorClasses.Update(professorClasses);
			await _apiUniversityContext.SaveChangesAsync();
			return res.Entity;
		}
	}
}
