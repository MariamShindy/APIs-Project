using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; } = null!;
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&1t;,])(?!.*\\s).*$",
			ErrorMessage ="Password must have lower case and upper case chatracters and numbers and non alphanumeric and must be greater then 6 characters")]
	
        public string Password { get; set; } = null!;
		[Required]
		public string Phone { get; set; } = null!;

	}
}
