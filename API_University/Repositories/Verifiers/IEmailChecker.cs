namespace API_University.Repositories.Verifiers
{
	public interface IEmailChecker
	{
		bool EmailVerifier(string email);
	}
}
