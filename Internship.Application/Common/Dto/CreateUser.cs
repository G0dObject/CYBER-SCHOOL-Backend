namespace Internship.Api.Controllers
{
	public class CreateUser
	{
		public string Email { get; set; }
		public string City { get; set; }
		public int Age { get; set; }
		public bool Maried { get; set; }
		public bool HaveChild { get; set; }
		public Directions Direction { get; set; }
		public Purpose Purpose { get; set; }
		public Region DesiredRegion { get; set; }
		public Education Education { get; set; }
	}
}