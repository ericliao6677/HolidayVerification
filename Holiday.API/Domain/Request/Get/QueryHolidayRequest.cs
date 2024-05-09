using System.ComponentModel.DataAnnotations;

namespace Holiday.API.Domain.Request.Get
{
    public class QueryHolidayRequest
    {

        public bool? IsHoliday { get; set; }
        public string? HolidayCategory { get; set; }
    }
}
