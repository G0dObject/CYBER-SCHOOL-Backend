using Internship.Domain.Enitity;
using Internship.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Internship.Application.Interfaces
{
	public interface IContext
	{
		public DbSet<Vacancy> Vacancies { get; set; }
		public DbSet<User> Users { get; set; }
		public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		public void Dispose();
		public DatabaseFacade Database { get; set; }

	}
}
