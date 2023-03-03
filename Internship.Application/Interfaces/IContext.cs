using Internship.Domain.Enitity;
using Microsoft.EntityFrameworkCore;

namespace Internship.Application.Interfaces
{
	public interface IContext
	{
		public DbSet<Vacancy> Vacancies { get; set; }

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		public void Dispose();

	}
}
