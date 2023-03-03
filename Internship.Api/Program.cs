using Internship.Application.Interfaces;
using Internship.DependencyInjection;
using Internship.Domain.Identity;
using Internship.Persistence;
using Internship.Persistence.Repositories;
using Internship.Persistence.UnitOfWork;
using Judemy.Api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Internship.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			_ = builder.Services.AddControllers()
	.AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
			_ = builder.Services.AddEndpointsApiExplorer();
			_ = builder.Services.AddSwaggerGen();

			_ = builder.Services.AddIdentityInjection();

			_ = builder.Services.AddAuthorization();
			_ = builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer");
			_ = builder.Services.AddAuthorizationBuilder().AddPolicy("admin_p", policy => policy.RequireRole("Admin"));
			_ = builder.Services.AddDbContext<IContext, Context>();
			_ = builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			_ = builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				_ = app.UseSwagger();
				_ = app.UseSwaggerUI();
			}

			_ = app.UseHttpsRedirection();

			_ = app.UseAuthorization();
			_ = app.MapControllers();


			Claim claim = new Claim("Name", "Sasha");
			_ = app.MapGet("/", (IJwtTokenGenerator jwt) =>
			{
				JwtSecurityToken g = jwt.GenerateJwtToken(new List<Claim>() { claim });

				return new JwtSecurityTokenHandler().WriteToken(g);
			});



			app.Run();
		}
	}
}