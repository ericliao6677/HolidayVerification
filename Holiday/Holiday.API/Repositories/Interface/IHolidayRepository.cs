using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Response;

namespace Holiday.API.Repositories.Interface
{
    public interface IHolidayRepository
    {
        Task<IEnumerable<HolidayEntity>> GetAsync(DateTime Date);

    }
}
