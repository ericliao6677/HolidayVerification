namespace Holiday.API.Domain.Request.Put
{
    public class PutHolidayRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string HolidayCategory { get; set; }
        public string? Description { get; set; }
    }
}
