using API_University.Data;

namespace API_University.Repositories
{
    public interface IProfessorRepository
    {
        Task<Professor> Get(int id);

		    int GetLastId();

		    Task<IEnumerable<Professor>> GetAll();

        Task<Professor> Create(Professor professor);

        Task<Professor> Update(Professor professor);
    }
}
