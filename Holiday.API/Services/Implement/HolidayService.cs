using AutoMapper;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
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
        if (result is null) return ResultResponseExtension.Command.QueryNotFound(date.Date.ToString());
        return ResultResponseExtension.Query.QuerySuccess(result);
    }

    public async Task<ResultResponse> InsertAsync(PostHolidayRequest request)
    {
        var entity = _mapper.Map<HolidayEntity>(request);
        var result = await _holidayRepository.InserAsync(entity);
        if (!result) return ResultResponseExtension.Command.InsertFail();
        return ResultResponseExtension.Command.InsertSuccess();
    }
}

