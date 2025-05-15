using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReactWebApi.Models;
using ReactWebApi.Repository;

namespace ReactWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : Controller
	{
		private readonly IAccountRepository _accountRepository;

		private readonly ILogger<AccountController> _logger;

		public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
			_accountRepository = accountRepository;
			_logger = logger;
		}

		[HttpGet("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _accountRepository.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUerbyId([FromRoute] int id)
		{
			try
			{
				var users = await _accountRepository.GetUserbyIdAsync(id);
				if (users == null)
				{
					_logger.LogWarning("User is null! means No Data Found.........");
					return NotFound();
				}
				return Ok(users);
			}
			catch (Exception ex)
			{
				//return BadRequest(ex.Message);
				return UnprocessableEntity(ex.Message);
			}
		}

		[HttpPost("signup")]
		public async Task<IActionResult> SignupUser([FromBody] UserModel userModel)
		{
			var id = await _accountRepository.SignupUserAsync(userModel);

			////below line means once post done then get the book back which is posted!
			return CreatedAtAction(nameof(GetUerbyId), new { id = id, controller = "Account" }, id);
		}

		[HttpPost("login")]

		public async Task<IActionResult> LoginUser([FromBody] LoginModel loginModel)
		{
			//_logger.LogInformation("Login Method Started................");
			var result = await _accountRepository.LoginUserAsync(loginModel);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody]UserModel userModel)
		{
			await _accountRepository.UpdateUserAsync(id, userModel);
			return Ok("Updated Successfully");
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateUserPatch([FromRoute]int id, [FromBody]JsonPatchDocument userModel)
		{
			await _accountRepository.UpdateUserPatchAsync(id, userModel);
			return Ok("Updated Patch Successfully");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser([FromRoute] int id)
		{
			await _accountRepository.DeleteUserAsync(id);
			return Ok("User Deleted Successfully");
		}

	}
}
