using Internship.Application.Interfaces;
using Internship.Application.Interfaces.Repository;
using Internship.Domain.Enitity;
using Microsoft.EntityFrameworkCore;

namespace Internship.Persistence.Repositories
{
	internal class VacancyRepository : Repository, IVacancyRepository
	{
		private new readonly IContext _context;
		public VacancyRepository(IContext context) : base(context)
		{
			_context = context;
		}

		public async Task CreateAsync(Vacancy entity)
		{
			await _context.Vacancies.AddAsync(entity);
			await Save();

		}

		public async Task Delete(int id)
		{
			_context.Vacancies.Remove(await GetByIdAsync(id) ?? throw new NullReferenceException());
			await Save();

		}

		public async Task<Vacancy?> FirstAsync()
		{
			return await _context.Vacancies.FirstOrDefaultAsync();
		}

		public async Task<List<Vacancy>> GetAllAsync()
		{
			return await _context.Vacancies.ToListAsync();
		}

		public async Task<Vacancy?> GetByIdAsync(int id)
		{
			return await _context.Vacancies.FirstOrDefaultAsync(v => v.Id == id);
		}

		public async Task<Vacancy?> LastAsync()
		{
			return await _context.Vacancies.LastOrDefaultAsync();
		}

		public async Task UpdateAsync(Vacancy entity)
		{
			_context.Vacancies.Update(entity);
			await Save();
		}
	}
}
