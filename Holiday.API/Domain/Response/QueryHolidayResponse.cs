namespace Holiday.API.Domain.Response
{
    public class QueryHolidayResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string? Name { get; set; }
        public bool IsHoliday { get; set; }
        public string HolidayCategory { get; set; }
        public string? Discription { get; set; }
    }
}
