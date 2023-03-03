using Microsoft.AspNetCore.Identity;

namespace Internship.Domain.Identity
{
	public class User : IdentityUser<int>
	{


		public User(string city, int age, bool maried, bool haveChild, Directions direction, Purpose purpose, Region desiredRegion, Education education)
		{
			City = city;
			Age = age;
			Maried = maried;
			HaveChild = haveChild;
			Direction = direction;
			Purpose = purpose;
			DesiredRegion = desiredRegion;
			Education = education;
		}

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
