using Internship.Application.Interfaces.Repository;

namespace Internship.Persistence.Repositories
{
	public interface IUnitOfWork
	{

		public IVacancyRepository Vacancy { get; set; }
		public void Dispose();

		Task Save();
	}
}