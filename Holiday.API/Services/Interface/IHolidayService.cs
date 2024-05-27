using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Request.Put;
using Holiday.API.Domain.Response;

namespace Holiday.API.Services.Interface
{
    public interface IHolidayService
    {
        //Task<ResultResponse> GetAsync(QueryHolidayRequest request);

        Task<ResultResponse<IEnumerable<QueryHolidayResponse>>> GetAsync(QueryHolidayRequest request);

        Task<ResultResponse<QueryHolidayResponse>> GetByDateAsync(string date);

        Task<ResultResponse<QueryHolidayResponse>> GetByIdAsync(int id);

        Task<ResultResponse> InsertAsync(PostHolidayRequest request);

        Task<ResultResponse> DeletebyIdAsync(int id);

        Task<ResultResponse> UpdateAsync(PutHolidayRequest request);

        Task<ResultResponse> ParseCsvFile(IFormFile file);

        Task<ResultResponse<IEnumerable<QueryHolidayResponse>>> GetUploadFileDataAsync();
    }
}
