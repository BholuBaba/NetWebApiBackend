using System.ComponentModel.DataAnnotations;

namespace ReactWebApi.Models
{
	public class SignupModel
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }
		
		[Required]
		public string Email { get; set; }

		[Required]
		[Compare("ConfirmPassword")]
		public string Password { get; set; }

		[Required]
		public string ConfirmPassword { get; set; }
	}
}
