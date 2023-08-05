using API_University.Data;
using API_University.DTOs.Professor;

namespace API_University.Repositories
{
    public interface IProfessorRepository
    {
        Task<Professor> Get(int id);

		    int GetLastId();

		    Task<List<ProfessorDto>> GetAll();

        Task<Professor> Create(Professor professor);

        Task<ProfessorDto> Update(Professor professor);
    }
}
