using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ReactWebApi.Context;
using ReactWebApi.Models;

namespace ReactWebApi.Repository
{
	public class AccountRepository : IAccountRepository
	{
		private readonly UserContext _userContext;

		public AccountRepository(UserContext userContext)
        {
			_userContext = userContext;
		}

		public async Task<List<UserModel>> GetAllUsersAsync()
		{
			var allUsers = await _userContext.users.Select(x => new UserModel()
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				Password = x.Password,
				ConfirmPassword = x.ConfirmPassword
			}).ToListAsync();

			return allUsers;
		}

		public async Task<UserModel> GetUserbyIdAsync(int userId)
		{
			var user = await _userContext.users.Where(x => x.Id == userId).Select(x => new UserModel()
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				Password = x.Password,
				ConfirmPassword = x.ConfirmPassword
			}).FirstOrDefaultAsync();

			return user;
		}

		public async Task<int> SignupUserAsync(UserModel userModel)
		{
			var user = new User()
			{
				Name = userModel.Name,
				Email = userModel.Email,
				Password = userModel.Password,
				ConfirmPassword = userModel.ConfirmPassword
			};
			_userContext.users.Add(user);
			await _userContext.SaveChangesAsync();

			return user.Id;
		}
		public async Task<string> LoginUserAsync(LoginModel loginModel)
		{
			var user = await _userContext.users
				.Where(y => y.Email == loginModel.Email && y.Password == loginModel.Password)
				.Select(x => new UserModel()
				{
					Id = x.Id,
					Name = x.Name,
					Email = x.Email,
					Password = x.Password,
					ConfirmPassword = x.ConfirmPassword
				}).FirstOrDefaultAsync(); ;

			if (user != null)
			{
				//string token = "paharitola525rajahindustani";
				return user.Name;
			}

			//if login successfully then get token = gfftyyuy65654657667fhvhh;

			return "Invalid User";
		}

		public async Task UpdateUserAsync(int userId, UserModel userModel)
		{
			var user = new User()
			{
				Id = userId,
				Name = userModel.Name,
				Email = userModel.Email,
				Password = userModel.Password,
				ConfirmPassword = userModel.ConfirmPassword,
			};
			_userContext.users.Update(user);
			await _userContext.SaveChangesAsync();
		}

		public async Task UpdateUserPatchAsync(int userId, JsonPatchDocument userModel)
		{
			var user = await _userContext.users.FindAsync(userId);
			if (user != null)
			{
				userModel.ApplyTo(user);
				await _userContext.SaveChangesAsync();
			}
		}

		public async Task DeleteUserAsync(int userId)
		{
			var user = new User() { Id = userId };
			_userContext.users.Remove(user);
			await _userContext.SaveChangesAsync();
		}

		public string TestingAPIPostAsync(string name)
		{
			return  "Hi " + name + " Welcome to Post Method";
		}

		public string TestingAPIGetAsync()
		{
			return "Welcome to Get Method";
		}
	}
}
