namespace Internship.Domain.Enitity
{
    public class Vacancy : IEntityBase
    {
        public int Id { get; set; }
        public Directions Directions { get; set; }
        public string Describe { get; set; }
        public Region Region { get; set; }
        public decimal Salary { get; set; }
        public string Experience { get; set; } = string.Empty;
    }
}
