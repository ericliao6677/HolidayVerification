namespace Holiday.API.Domain.DTO
{
    public class CsvFileDto
    {
        public string Date { get; set; }
        public string? Name { get; set; }
        public string IsHoliday { get; set; }
        public string HolidayCategory { get; set; }
        public string? Description { get; set; }
    }
}
