using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories
{
	public class StudentRepository : IStudentRepository
	{
		private readonly ApiUniversityContext _apiUniversityContext;

		public StudentRepository(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}

		public async Task<Student> Create(Student student)
		{
			var res = _apiUniversityContext.Add(student);
			await _apiUniversityContext.SaveChangesAsync();

			return res.Entity;
		}

		public int GetLastId()
		{
			var lastId = _apiUniversityContext.Students.OrderByDescending(x => x.Id).FirstOrDefault();

			if (lastId != null)
			{
				return lastId.Id;
			}

			return 0;
		}

		public bool VerifyExistence(string name)
		{
			var existeCampo = _apiUniversityContext.Students.Any(x => x.Name == name);
			return existeCampo;
		}

		public async Task<Student> Get(int id)
		{
			var student = await _apiUniversityContext.Students.FirstOrDefaultAsync(a => a.Id == id);

			if (student == null)
				throw new Exception($"El id '{id}' que pasaste no existe.");
			else return student;
    }

		public async Task<IEnumerable<Student>> GetAll()
		{
			return await _apiUniversityContext.Students.ToListAsync();
		}

		public async Task<Student> Update(Student student)
		{
			var res = _apiUniversityContext.Students.Update(student);
			await _apiUniversityContext.SaveChangesAsync();
			return res.Entity;
		}
	}
}
