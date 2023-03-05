using CodeBits;
using Internship.Api.Services;
using Internship.Application.Common.Dto;
using Internship.Domain.Identity;
using Internship.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Internship.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly RoleManager<Role> _roleManager;


		public LoginController(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator, RoleManager<Role> roleManager)
		{
			_userManager = userManager;
			_jwtTokenGenerator = jwtTokenGenerator;
			_roleManager = roleManager;


		}
		[Authorize]
		[HttpGet]
		public string GetSecret()
		{
			return "Secret";
		}
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginUser model)
		{
			User? user = await _userManager.FindByNameAsync(model.Login);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				IList<string> userRoles = await _userManager.GetRolesAsync(user);

				List<Claim> authClaims = new()
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
				};

				foreach (string? userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}
				JwtSecurityToken? token = _jwtTokenGenerator.GenerateJwtToken(authClaims);

				return Ok(new
				{
					username = user.UserName,
					token = new JwtSecurityTokenHandler().WriteToken(token),
				});
			}
			return Unauthorized();
		}

		[HttpPost]
		[Route("SetAdmin")]
		public async Task<IActionResult> SetAdmin(int id)
		{
			IdentityResult g = await _roleManager.CreateAsync(new Role() { Name = "Admin" });
			IdentityResult c = await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(id.ToString()), "Admin");
			return Ok(c);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register(CreateUser model)
		{
			User userExists = await _userManager.FindByEmailAsync(model.Email);
			if (userExists != null)
				return StatusCode(StatusCodes.Status409Conflict, "Alredy exist");


			string FullName = model.FirstName + " " + model.LastName + " " + model.Surname;
			User user = new(
					fullName: FullName,
					dayOfBird: model.DayOfBird,
					city: model.City,
					direction: model.Direction,
					adress: model.Adress,
					education: model.Education,
					maried: model.Maried,
					haveChild: model.HaveChild,
					desiredRegion: model.DesiredRegion
				   )
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = PasswordGenerator.Generate(10, PasswordCharacters.AllLetters | PasswordCharacters.Numbers, new char[] { ' ' })
			};
			string password = PasswordGenerator.Generate(10, PasswordCharacters.AllLetters | PasswordCharacters.Numbers, new char[] { ' ' });
			IdentityResult result = await _userManager.CreateAsync(user, password);

			var response = new { Login = user.UserName, Password = password };
			return result.Succeeded
				? StatusCode(StatusCodes.Status201Created, response) :
				StatusCode(StatusCodes.Status400BadRequest, "Create Failed");
		}
	}
}