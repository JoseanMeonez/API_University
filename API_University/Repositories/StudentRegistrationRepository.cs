using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
	public class StudentRegistrationRepository : IStudentRegistrationRepository
	{
		private readonly ApiUniversityContext _apiUniversityContext;

		public StudentRegistrationRepository(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}

		public async Task<StudentRegistration> Create(StudentRegistration studentRegistrations)
		{
			var res = _apiUniversityContext.Add(studentRegistrations);
			await _apiUniversityContext.SaveChangesAsync();

			return res.Entity;
		}

		public Task<StudentRegistration> Get(int id)
		{
			return _apiUniversityContext.StudentRegistrations.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<IEnumerable<StudentRegistration>> GetAll()
		{
			return await _apiUniversityContext.StudentRegistrations.ToListAsync();
		}

		public int CountById(int id)
		{
			int count = _apiUniversityContext.StudentRegistrations.Count(x => x.StudentId == id);
			return count;
		}

		public async Task<StudentRegistration> Update(StudentRegistration studentRegistrations)
		{
			var res = _apiUniversityContext.StudentRegistrations.Update(studentRegistrations);
			await _apiUniversityContext.SaveChangesAsync();
			return res.Entity;
		}
	}
}
