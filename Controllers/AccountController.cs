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

		[HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupModel signupModel)
		{
			_logger.LogInformation("Signup Method Started................");
			try
			{
				var result = _accountRepository.SignupAsync(signupModel);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpPost("login")]

		public IActionResult Login([FromBody] LoginModel loginModel)
		{
			_logger.LogInformation("Login Method Started................");
			var result = _accountRepository.LoginAsync(loginModel);
			return Ok(result);
		}

	}
}
