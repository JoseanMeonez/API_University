using API_University.Data;
using API_University.DTOs.Professor;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
  public class ProfessorRepository : IProfessorRepository
  {
    private readonly ApiUniversityContext _apiUniversityContext;
		private readonly IMapper _mapper;

		public ProfessorRepository(ApiUniversityContext _context, IMapper mapper)
		{
			_apiUniversityContext = _context;
			_mapper = mapper;
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

		public async Task<Professor> Get(int id)
    {
			var professor = await _apiUniversityContext.Professors.FirstOrDefaultAsync(a => a.Id == id);

      if (professor == null)
				throw new Exception($"El id '{id}' que pasaste no existe.");
			else return _mapper.Map<Professor>(professor);
		}

    public async Task<List<ProfessorDto>> GetAll()
    {
      var professor = await _apiUniversityContext.Professors.ToListAsync();
      return _mapper.Map<List<ProfessorDto>>(professor);
    }

    public async Task<ProfessorDto> Update(Professor professor)
    {
      var res = _apiUniversityContext.Professors.Update(professor);
      await _apiUniversityContext.SaveChangesAsync();
      return _mapper.Map<ProfessorDto>(res.Entity);
    }
  }
}
