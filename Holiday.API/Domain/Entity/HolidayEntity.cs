namespace Holiday.API.Domain.Entity
{
    public class HolidayEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public bool? IsHoliday { get; set; }
        public string HolidayCategory { get; set; }
        public string? Discription { get; set; }
    }
}
