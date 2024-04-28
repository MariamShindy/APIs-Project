using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Controllers
{

	public class AccountController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return Unauthorized(new ApiResponse(401 , "Invalid login"));
			}
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
			if(!result.Succeeded)
				return Unauthorized(new ApiResponse(401, "Invalid login"));
			return Ok(new UserDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = "this is token"
			});
		}
		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model )
		{
			var user = new ApplicationUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				PhoneNumber = model.Phone,
				UserName = model.Email.Split("@")[0],
			};
			var result = await _userManager.CreateAsync(user , model.Password);
			if(!result.Succeeded)
				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) });
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = "this is token"
			});
		}
	}
}
