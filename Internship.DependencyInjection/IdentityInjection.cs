using Internship.Domain.Identity;
using Internship.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Internship.DependencyInjection
{
	public static class IdentityInjection
	{
		public static IServiceCollection AddIdentityInjection(this IServiceCollection services)
		{
			IdentityBuilder? builder = services.AddIdentity<User, Role>(option =>
			{
				option.User.RequireUniqueEmail = true;

				option.Stores.MaxLengthForKeys = 128;
				option.Password.RequireUppercase = false;
				option.Password.RequireNonAlphanumeric = false;
				option.Password.RequireDigit = false;

				option.SignIn.RequireConfirmedPhoneNumber = false;
				option.SignIn.RequireConfirmedEmail = false;
				option.SignIn.RequireConfirmedAccount = false;
			}).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();


			services.AddIdentityCore<User>();
			return services;
		}
	}
}
