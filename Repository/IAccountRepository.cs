using ReactWebApi.Models;

namespace ReactWebApi.Repository
{
	public interface IAccountRepository
	{
		public string SignupAsync(SignupModel signupModel);
		public string LoginAsync(LoginModel loginModel);

	}
}
