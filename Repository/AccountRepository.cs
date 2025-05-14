using ReactWebApi.Models;

namespace ReactWebApi.Repository
{
	public class AccountRepository : IAccountRepository
	{
		public string SignupAsync(SignupModel signupModel)
		{
			return "Signup Successfully";
		}
		public string LoginAsync(LoginModel loginModel)
		{
			if (loginModel.Email == "Farhad@Test.com" && loginModel.Password == "Test123")
			{
				return "Logged In Successfully";
			}
			else
			{
				return "Log In Failed";
			}
		}
	}
}
