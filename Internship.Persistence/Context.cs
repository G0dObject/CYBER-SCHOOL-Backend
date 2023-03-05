using Internship.Application.Interfaces;
using Internship.Domain.Enitity;
using Internship.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Internship.Persistence
{
	public class Context : IdentityDbContext<User, Role, int>, IContext
	{
		public Context()
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();

		}


		public DbSet<Vacancy> Vacancies { get; set; }
		DatabaseFacade IContext.Database { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("DataSource=Api.db");
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

	}
}
