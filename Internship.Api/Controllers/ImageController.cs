
using Internship.Api.Services.Images;
using Internship.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Linq;

namespace Internship.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly ImageService _imageService;
		private UserManager<User> _usermanager;
		public ImageController(ImageService imageService, UserManager<User> usermanager)
		{
			_usermanager = usermanager;
			_imageService = imageService;
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			string? userid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
				   .Select(c => c.Value).SingleOrDefault();
			User? user = await _usermanager.FindByIdAsync(userid);
			if (user == null)
			{
				return NotFound();
			}
			await _imageService.UploadImage(file, user);
			return Ok();
		}

		[HttpGet]
		[Authorize]

		public async Task<IActionResult> GetImage()
		{
			string? userid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
				   .Select(c => c.Value).SingleOrDefault();
			User? user = await _usermanager.FindByIdAsync(userid);
			try
			{
				byte[]? image = await _imageService.GetImage(user);
				if (image != null)
				{
					return File(image, "image/jpeg");
				}
				return StatusCode(204);
			}
			catch (Exception)
			{
				return null;
			}
		}
		[Route("Check")]
		[HttpGet]
		[Authorize]
		public async Task<bool> ImageExist()
		{
			string? userid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
				   .Select(c => c.Value).SingleOrDefault();
			User? user = await _usermanager.FindByIdAsync(userid);

			return _imageService.ImageExist(user);
		}
		[HttpDelete]
		[Authorize]
		public async Task DeleteImage()
		{
			string? userid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
				   .Select(c => c.Value).SingleOrDefault();
			User? user = await _usermanager.FindByIdAsync(userid);
			_imageService.DeleteImage(user);
		}
	}
}
