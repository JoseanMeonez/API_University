using API_University.Data;

namespace API_University.Repositories
{
	public interface IClassroomRepository
	{
		Task<Classroom> Get(int id);

		int GetLastId();

		Task<IEnumerable<Classroom>> GetAll();

		Task<Classroom> Create(Classroom classrooms);

		Task<Classroom> Update(Classroom classrooms);
	}
}
