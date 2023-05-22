using AssetManager.Database;
using AssetManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManager.Controllers
{
	/// <summary>
	/// The controller responsible for managing the creation and deletion of users aswell as Authenthication
	/// </summary>
	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly MainDatabase dbContext;

		public UserController(MainDatabase dbContext, IConfiguration configuration)
		{
			this.dbContext = dbContext;
			this.configuration = configuration;
		}

		/// <summary>
		/// Allows user to remove their Account
		/// </summary>
		/// <returns>Nothing</returns>
		[HttpDelete()]
		[Authorize]
		[ProducesResponseType(201)]
		[ProducesResponseType(401)]
		public ActionResult Delete()
		{
			User? user = dbContext.Users.FirstOrDefault(a => a.Name == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			if (user is null)
				return Unauthorized();

			_ = dbContext.Users.Remove(user);

			dbContext.SaveChanges();
			return NoContent();
		}

		/// <summary>
		/// Allows the user to log in
		/// </summary>
		/// <param name="parameters">The shema of data required for a users login</param>
		/// <returns> A jwt token to be used for Authorization </returns>
		[HttpPost("Login")]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(404)]
		public ActionResult Login([FromBody] AuthenticationParams parameters)
		{
			User? user = Authenthicate(parameters);

			if (user is null)
				return NotFound();

			return Ok(GenerateToken(user));
		}

		/// <summary>
		/// Allows to create a new user
		/// </summary>
		/// <param name="parameters">The shema of data required for a users login</param>
		/// <returns>Nothing :)</returns>
		[HttpPost("Register")]
		[ProducesResponseType(200)]
		public ActionResult Register([FromBody] AuthenticationParams parameters)
		{
			User newUser = new()
			{
				Name = parameters.Name,
				Password = parameters.Password
			};

			dbContext.Users.Add(newUser);
			dbContext.SaveChanges();
			return Ok();
		}

		/// <summary>
		/// Change the password for the given user
		/// </summary>
		/// <param name="parameters"> The new password for the user</param>
		/// <returns>Nothing</returns>
		[HttpPut()]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public ActionResult Update([FromBody] PasswordChangeParams passwordChangeParams)
		{
			User? user = dbContext.Users.FirstOrDefault(a => a.Name == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			if (user is null)
				return Unauthorized();

			user.Password = passwordChangeParams.Password;
			dbContext.SaveChanges();

			return Ok();
		}

		private User? Authenthicate(AuthenticationParams parameters)
		{
			return dbContext.Users.FirstOrDefault(a => a.Name == parameters.Name && a.Password == parameters.Password);
		}

		private string GenerateToken(User user)
		{
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["JWT:key"] ?? "screm"));
			SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);
			Claim[] claims = {
				new Claim(ClaimTypes.NameIdentifier, user.Name)
			};

			JwtSecurityToken token = new(configuration["JWT:Issuer"], configuration["JWT:Audience"], claims, signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public sealed class AuthenticationParams
		{
			public string Name { get; set; }
			public string Password { get; set; }
		}

		public sealed class PasswordChangeParams
		{
			public string Password { get; set; }
		}
	}
}