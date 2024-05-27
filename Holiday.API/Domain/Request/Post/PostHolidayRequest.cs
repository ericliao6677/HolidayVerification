using System.ComponentModel.DataAnnotations;

namespace Holiday.API.Domain.Request.Post
{
    public class PostHolidayRequest
    {
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "名稱欄位必填")]
        public string? Name { get; set; }
        public string IsHoliday { get; set; }
        public string HolidayCategory { get; set; }
        public string? Description { get; set; }
    }
}
