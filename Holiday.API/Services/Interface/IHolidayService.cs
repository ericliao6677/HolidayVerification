using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Response;

namespace Holiday.API.Services.Interface
{
    public interface IHolidayService
    {
        Task<ResultResponse> GetAsync(QueryHolidayRequest request);
        
        Task<ResultResponse> GetByDateAsync(DateTime date);

        Task<ResultResponse> InsertAsync(PostHolidayRequest request);
    }
}
