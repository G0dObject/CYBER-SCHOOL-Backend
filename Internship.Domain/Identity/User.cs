using Microsoft.AspNetCore.Identity;

namespace Internship.Domain.Identity
{
	public class User : IdentityUser<int>
	{
		private Directions direction;
		private Education education;
		private Region desiredRegion;

		public User(string fullName, DateTime dayOfBird, string city, Directions direction, string adress, Education education, bool maried, bool haveChild, Region desiredRegion)
		{
			FullName = fullName;
			DayOfBird = dayOfBird;
			City = city;
			Direction = direction;
			Adress = adress;
			Education = education;
			Maried = maried;
			HaveChild = haveChild;
			DesiredRegion = desiredRegion;
		}

		public string FullName { get; set; }
		public DateTime DayOfBird { get; set; }
		public string City { get; set; }
		public Directions Direction { get; set; }
		public string Adress { get; set; }
		public Education Education { get; set; }
		public bool Maried { get; set; }
		public bool HaveChild { get; set; }
		public Region DesiredRegion { get; set; }
	}
}
