using API_University.Data;
using Microsoft.EntityFrameworkCore;

namespace API_University.Repositories.Verifiers
{
  public class StudentChecker : INameChecker, IPhoneNumberChecker, IEmailChecker
	{
    private readonly ApiUniversityContext _apiUniversityContext;

    public StudentChecker(ApiUniversityContext _context)
    {
      _apiUniversityContext = _context;
    }

		public bool CellphoneVerifier(string cel)
		{
			var field = _apiUniversityContext.Students.Any(x => x.CelNumber == cel);
			return field;
		}

		public bool EmailVerifier(string email)
		{
			var field = _apiUniversityContext.Students.Any(x => x.Email == email);
			return field;
		}

		public bool NameVerifier(string name)
		{
			var field = _apiUniversityContext.Students.Any(x => x.Name == name);
			return field;
		}
	}
}
