using AutoMapper;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Request.Put;
using Holiday.API.Domain.Response;
using Holiday.API.Repositories.Interface;
using Holiday.API.Services.Interface;

namespace Holiday.API.Services.Implement;

public class HolidayService : IHolidayService
{
    private readonly IHolidayRepository _holidayRepository;
    private readonly IMapper _mapper;

    public HolidayService(IHolidayRepository holidayRepository, IMapper mapper)
    {
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse> GetAsync(QueryHolidayRequest? request)
    {
        var entity = _mapper.Map<HolidayEntity>(request);
        var result = await _holidayRepository.GetAsync(entity);
        return ResultResponseExtension.Query.QuerySuccess(result);
    }

    public async Task<ResultResponse> GetByDateAsync(DateTime date)
    {
        var result = await _holidayRepository.GetByDateAsync(date);
        if (result is null) return ResultResponseExtension.Command.QueryNotFound(date.ToString("yyyy/MM/dd"));
        return ResultResponseExtension.Query.QuerySuccess(result);
    }

    public async Task<ResultResponse> GetByIdAsync(int id)
    {
        var result = await _holidayRepository.GetByIdAsync(id);
        if (result is null) return ResultResponseExtension.Command.QueryNotFound(id.ToString());
        return ResultResponseExtension.Query.QuerySuccess(result);
    }

    public async Task<ResultResponse> InsertAsync(PostHolidayRequest request)
    {
        var entity = _mapper.Map<HolidayEntity>(request);
        var result = await _holidayRepository.InsertAsync(entity);
        if (!result) return ResultResponseExtension.Command.InsertFail();
        return ResultResponseExtension.Command.InsertSuccess();
    }

    public async Task<ResultResponse> DeletebyIdAsync(int id)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        if (holiday is null) return ResultResponseExtension.Command.QueryNotFound(id.ToString());

        var result = await _holidayRepository.DeleteByIdAsync(id);

        if (!result) return ResultResponseExtension.Command.DeleteFail();
        return ResultResponseExtension.Command.DeleteSuccess();

    }

    public async Task<ResultResponse> UpdateAsync(PutHolidayRequest request)
    {
        var entity = _mapper.Map<HolidayEntity>(request);
        var result = await _holidayRepository.UpdateAsync(entity);
        if (!result) return ResultResponseExtension.Command.UpdateFail();
        return ResultResponseExtension.Command.UpdateSuccess();
    }
}

