using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
  public class ProfessorRepository : IProfessorRepository
  {
    private readonly ApiUniversityContext _apiUniversityContext;

    public ProfessorRepository(ApiUniversityContext _context)
    {
        _apiUniversityContext = _context;
    }

    public async Task<Professor> Create(Professor professor)
    {
        var res = _apiUniversityContext.Add(professor);
        await _apiUniversityContext.SaveChangesAsync();

        return res.Entity;
    }

		public int GetLastId()
		{
			var lastId = _apiUniversityContext.Professors.OrderByDescending(x => x.Id).FirstOrDefault();

			if (lastId != null)
			{
				return lastId.Id;
			}

			return 0;
		}

		public Task<Professor> Get(int id)
    {
        return _apiUniversityContext.Professors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Professor>> GetAll()
    {
        return await _apiUniversityContext.Professors.ToListAsync();
    }

    public async Task<Professor> Update(Professor professor)
    {
        var res = _apiUniversityContext.Professors.Update(professor);
        await _apiUniversityContext.SaveChangesAsync();
        return res.Entity;
    }
  }
}
