using AutoMapper;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Request.Put;
using Holiday.API.Domain.Response;
using Holiday.API.Repositories.Interface;
using Holiday.API.Services.Interface;
using System.Collections.Generic;

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

    public async Task<ResultResponse<IEnumerable<QueryHolidayResponse>>> GetAsync(QueryHolidayRequest request)
    {
        var entity = _mapper.Map<HolidayEntity>(request);
        var result = await _holidayRepository.GetAsync(entity);
        
        var mappedResults = result.Select(item => { 
            var mappedResult = _mapper.Map<QueryHolidayResponse>(item);
            return mappedResult;
        });
        
        return ResultResponseExtension.Query.QuerySuccess(mappedResults);
    }

    public async Task<ResultResponse<QueryHolidayResponse>> GetByDateAsync(DateTime date)
    {
        var result = await _holidayRepository.GetByDateAsync(date);
        if (result is null) return ResultResponseExtension.Command.QueryNotFound<QueryHolidayResponse>(date.ToString("yyyy/MM/dd"));

        var mappedResult = _mapper.Map<QueryHolidayResponse>(result);
        return ResultResponseExtension.Query.QuerySuccess(mappedResult);
    }

    public async Task<ResultResponse<QueryHolidayResponse>> GetByIdAsync(int id)
    {
        var result = await _holidayRepository.GetByIdAsync(id);
        if (result is null) return ResultResponseExtension.Command.QueryNotFound<QueryHolidayResponse>(id.ToString());

        var mappedResult = _mapper.Map<QueryHolidayResponse>(result);
        return ResultResponseExtension.Query.QuerySuccess(mappedResult);
    }

    public async Task<ResultResponse> InsertAsync(PostHolidayRequest request)
    {
        var entity = _mapper.Map<HolidayEntity>(request);
        entity.IsHoliday = true;
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

