using API_University.Data;

namespace API_University.Repositories
{
	public interface IStudentRegistrationRepository
	{
		int CountById(int id);

		Task<StudentRegistration> Get(int id);

		Task<IEnumerable<StudentRegistration>> GetAll();

		Task<StudentRegistration> Create(StudentRegistration classes);

		Task<StudentRegistration> Update(StudentRegistration classes);
	}
}
