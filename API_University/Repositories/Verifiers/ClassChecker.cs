using API_University.Data;

namespace API_University.Repositories.Verifiers
{
	public class ClassChecker : INameChecker
	{
		private readonly ApiUniversityContext _apiUniversityContext;

		public ClassChecker(ApiUniversityContext _context)
		{
			_apiUniversityContext = _context;
		}

		public bool NameVerifier(string name)
		{
			var field = _apiUniversityContext.Classes.Any(x => x.Name == name);
			return field;
		}
	}
}
