
using Internship.Application.Interfaces;

namespace Internship.Persistence.Repositories
{
	public class Repository
	{
		protected readonly IContext _context;

		public Repository(IContext context)
		{
			_context = context;
		}
		public async Task Save() => await _context.SaveChangesAsync(new CancellationToken());
	}
}
