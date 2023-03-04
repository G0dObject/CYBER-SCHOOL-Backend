using Internship.Domain.Enitity;
using Internship.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Internship.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacancyController : ControllerBase
	{
		private IUnitOfWork _unitOfWork;

		public VacancyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet("{id?}")]
		public async Task<Vacancy> Get(int id) => await _unitOfWork.Vacancy.GetByIdAsync(id) ?? throw new Exception($"sequence not contains vacancy with id:{id}");
		[HttpGet]
		public async Task<List<Vacancy>> Get() => await _unitOfWork.Vacancy.GetAllAsync();

		[Authorize(Policy = "admin_p")]
		[HttpPost]
		[Authorize("admin_p")]
		public async Task Create(Vacancy vacancy) => await _unitOfWork!.Vacancy!.CreateAsync(vacancy);

		[Authorize(Policy = "admin_p")]
		[HttpDelete]
		[Authorize("admin_p")]
		public async Task Delete(int id) => await _unitOfWork.Vacancy.Delete(id);

		[Authorize(Policy = "admin_p")]
		[HttpPut]
		[Authorize("admin_p")]
		public async Task Update(Vacancy vacancy) => await _unitOfWork.Vacancy.UpdateAsync(vacancy);

	}
}
