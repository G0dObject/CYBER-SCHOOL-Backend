using Internship.Application.Interfaces;
using Internship.Application.Interfaces.Repository;
using Internship.Persistence.Repositories;

namespace Internship.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private bool _disposed = false;
		private IContext _context;
		public UnitOfWork(IContext context)
		{
			_context = context;
			Vacancy = new VacancyRepository(_context);
		}
		public IVacancyRepository Vacancy { get; set; }

		public void Dispose()
		{
			if (!_disposed)
			{
				_context.Dispose();
				_disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public async Task Save() => _ = await _context.SaveChangesAsync(new CancellationToken());
		~UnitOfWork() => Dispose();
	}
}
