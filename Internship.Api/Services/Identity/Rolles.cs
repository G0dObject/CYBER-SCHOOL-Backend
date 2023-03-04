using Internship.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Internship.Api.Services.Identity
{
	public static class Rolles
	{
		public async static Task AddRoles(this IServiceProvider services, string[] roles)
		{
			IServiceProvider scope = services.CreateScope().ServiceProvider;
			RoleManager<Role> _roleManager = scope.GetRequiredService<RoleManager<Role>>();
			UserManager<User> _userManager = scope.GetRequiredService<UserManager<User>>();
			foreach (string role in roles)
			{
				bool roleExist = await _roleManager.RoleExistsAsync(role);
				if (!roleExist)
				{
					IdentityResult roleResult = await _roleManager.CreateAsync(new Role()
					{
						Name = role,
						ConcurrencyStamp = Guid.NewGuid().ToString()
					});
				}
			}
		}
	}
}