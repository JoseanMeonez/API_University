using API_University.Data;

namespace API_University.Repositories
{
	public interface IProfessorClassRepository
	{
		int CountById(int id);

		Task<ProfessorClass> Get(int id);

		Task<IEnumerable<ProfessorClass>> GetAll();
		
		Task<ProfessorClass> Create(ProfessorClass classes);

		Task<ProfessorClass> Update(ProfessorClass classes);
	}
}
