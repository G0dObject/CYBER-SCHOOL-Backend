using Internship.Api.Services;
using Internship.Api.Services.Identity;
using Internship.Api.Services.Images;
using Internship.Application.Interfaces;
using Internship.DependencyInjection;
using Internship.Persistence;
using Internship.Persistence.Repositories;
using Internship.Persistence.UnitOfWork;
using Internship.Persistent.DependencyInjection;
using System.Text.Json.Serialization;

namespace Internship.Api
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			_ = builder.Services.AddControllers()
	.AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
			_ = builder.Services.AddEndpointsApiExplorer();
			_ = builder.Services.AddSwaggerGen();

			_ = builder.Services.AddIdentityInjection();


			_ = builder.Services.AddAuthenticationDependency(builder.Configuration);
			_ = builder.Services.AddAuthorization();
			_ = builder.Services.AddAuthorizationBuilder().AddPolicy("admin_p", policy => policy.RequireRole("Admin"));

			_ = builder.Services.AddCors(options => options.AddPolicy(name: "local", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

			_ = builder.Services.AddDbContext<IContext, Context>();
			_ = builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			_ = builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
			_ = builder.Services.AddScoped<ImageService>();

			WebApplication app = builder.Build();


			_ = app.UseSwagger();
			_ = app.UseSwaggerUI();

			_ = app.UseHttpsRedirection();
			_ = app.UseAuthentication();
			_ = app.UseAuthorization();

			_ = app.MapControllers();
			_ = app.UseCors("local");
			await Rolles.AddRoles(app.Services, new[] { "Admin", "User", "Manager" });
			_ = app.MapGet("/", () => "Work").AllowAnonymous();
			_ = app.MapPost("/drop", (Context context) => context.Database.EnsureDeleted());
			app.Run();
		}
	}
}