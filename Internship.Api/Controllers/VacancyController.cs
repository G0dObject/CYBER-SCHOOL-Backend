using Internship.Application.Interfaces;
using Internship.Domain.Enitity;
using Internship.Persistence;
using Internship.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Internship.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacancyController : ControllerBase
	{

		private IContext _context;
		private IUnitOfWork _unitOfWork;


		public VacancyController(IContext context, IUnitOfWork unitOfWork)
		{
			_context = context;
			_unitOfWork = unitOfWork;
		}

		[HttpGet("{id?}")]
		[Authorize(Policy = "admin_p")]
		public async Task<Vacancy> Get(int id) => await _unitOfWork.Vacancy.GetByIdAsync(id) ?? throw new NullReferenceException();
		[HttpGet]
		[Authorize(Policy = "admin_p")]
		public async Task<List<Vacancy>> Get() => await _unitOfWork.Vacancy.GetAllAsync();

		[HttpPost]
		public async Task Create(Vacancy vacancy) => await _unitOfWork!.Vacancy!.CreateAsync(vacancy);

		[HttpDelete]
		public async Task Delete(int id) => await _unitOfWork.Vacancy.Delete(id);

		[HttpPut]
		public async Task Update(Vacancy vacancy) => await _unitOfWork.Vacancy.UpdateAsync(vacancy);

	}
}
