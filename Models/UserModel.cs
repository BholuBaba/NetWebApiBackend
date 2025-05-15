using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactWebApi.Models
{
	public class UserModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		[Compare("ConfirmPassword")]
		public string Password { get; set; }

		[Required]
		public string ConfirmPassword { get; set; }
	}
}
