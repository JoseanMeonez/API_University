using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
  public class ClassRepository : IClassRepository
  {
		private readonly ApiUniversityContext _apiUniversityContext;

		public ClassRepository(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}
		public async Task<Class> Create(Class classes)
		{
			var res = _apiUniversityContext.Add(classes);
			await _apiUniversityContext.SaveChangesAsync();

			return res.Entity;
		}

		public Task<Class> Get(int id)
		{
			return _apiUniversityContext.Classes.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<IEnumerable<Class>> GetAll()
		{
			return await _apiUniversityContext.Classes.ToListAsync();
		}

		public async Task<Class> Update(Class classes)
		{
			var res = _apiUniversityContext.Classes.Update(classes);
			await _apiUniversityContext.SaveChangesAsync();
			return res.Entity;
		}
	}
}
