using Microsoft.AspNetCore.JsonPatch;
using ReactWebApi.Models;

namespace ReactWebApi.Repository
{
	public interface IAccountRepository
	{
		public Task<List<UserModel>> GetAllUsersAsync();

		public Task<UserModel> GetUserbyIdAsync(int id);

		public Task<int> SignupUserAsync(UserModel userModel);

		public Task<string> LoginUserAsync(LoginModel loginModel);

		public Task UpdateUserAsync(int userId, UserModel userModel);

		public Task UpdateUserPatchAsync(int userId, JsonPatchDocument userModel);

		public Task DeleteUserAsync(int userId);

	}
}
