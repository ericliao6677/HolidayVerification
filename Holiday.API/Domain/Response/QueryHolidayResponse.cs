namespace Holiday.API.Domain.Response
{
    public class QueryHolidayResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string? Name { get; set; }
        public string IsHoliday { get; set; }
        public string HolidayCategory { get; set; }
        public string? Description { get; set; }
    }
}
