namespace Internship.Application.Common.Dto
{
	public class CreateUser
	{
		public string Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Surname { get; set; }
		public DateTime DayOfBird { get; set; }
		public string? City { get; set; }
		public Directions Direction { get; set; }
		public string? Adress { get; set; }
		public Education Education { get; set; }
		public bool Maried { get; set; }
		public bool HaveChild { get; set; }
		public Region DesiredRegion { get; set; }
	}
}