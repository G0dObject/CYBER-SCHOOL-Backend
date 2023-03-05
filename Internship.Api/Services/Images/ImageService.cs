using Internship.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Drawing;
using System.Text;

namespace Internship.Api.Services.Images
{
	public class ImageService
	{
		private UserManager<User> _userManager;

		public ImageService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task UploadImage(IFormFile file, User user)
		{
			string path;
			string fullpath;
			try
			{
				if (file.Length > 0)
				{
					path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
					if (!Directory.Exists(path))
						_ = Directory.CreateDirectory(path);
					fullpath = Path.Combine(path, user.UserName + ".jpg");
					using (FileStream fileStream = new(fullpath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}
					user.ImageLocation = fullpath;
					await _userManager.UpdateAsync(user);
				}
			}
			catch
			{
				throw;
			}

		}
		public async Task<byte[]?> GetImage(User user)
		{
			if (ImageExist(user))
			{
				string path = user.ImageLocation;
				return File.ReadAllBytes(path);
			}
			return null;
		}
		public bool ImageExist(User user)
		{
			if (File.Exists(user.ImageLocation))
			{
				string path = user.ImageLocation;
				return File.Exists(path);
			}
			return false;
		}
		public void DeleteImage(User user)
		{
			if (ImageExist(user))
			{
				string path = user.ImageLocation;
				File.Delete(path);
			}

		}
	}
}
