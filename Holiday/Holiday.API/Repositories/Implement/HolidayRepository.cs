using Holiday.API.Domain.Entity;
using Holiday.API.Repositories.Interface;

namespace Holiday.API.Repositories.Implement
{
    public class HolidayRepository : IHolidayRepository
    {
        public Task<IEnumerable<HolidayEntity>> GetAsync(DateTime Date)
        {
            throw new NotImplementedException();
        }
    }
}
