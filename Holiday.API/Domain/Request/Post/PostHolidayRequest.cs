namespace Holiday.API.Domain.Request.Post
{
    public class PostHolidayRequest
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string HolidayCategory { get; set; }
        public string? Description { get; set; }
    }
}
