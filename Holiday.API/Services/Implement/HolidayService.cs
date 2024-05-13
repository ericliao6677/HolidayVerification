using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Holiday.API.Common.Extension;
using Holiday.API.Domain.DTO;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Request.Put;
using Holiday.API.Domain.Response;
using Holiday.API.Repositories.Interface;
using Holiday.API.Services.Interface;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static Dapper.SqlMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        var mappedResults = result.Select(item =>
        {
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
        return ResultResponseExtension.Query.QuerySuccess<QueryHolidayResponse>(mappedResult);
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

    public async Task<ResultResponse> ParseCsvFile(IFormFile file)
    {
        try
        {
            var readConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var encoding = Encoding.GetEncoding("BIG-5");
        
            using Stream stream = file.OpenReadStream();

            using StreamReader reader = new StreamReader(stream, encoding);

            using CsvReader csv = new CsvReader(reader, readConfiguration);

            var records = csv.GetRecords<CsvFileDto>().ToList();


            //檢查日期格式
            foreach (var record in records)
            {
                var formatDateString = FormatDateExtension.FormatDate(record.Date);
                if (!DateTime.TryParse(formatDateString, out var formatDateValue))
                    return ResultResponseExtension.Command.InsertFail($"資料日期欄位不符，從第{records.IndexOf(record) + 2}開始");
            }

            //mapping資料
            var recordsMapped = records.Select(r =>
            {
                var entity = _mapper.Map<HolidayEntity>(r);
                entity.IsHoliday = r.IsHoliday == "是" ? true : false;
                entity.Date = DateTime.Parse(FormatDateExtension.FormatDate(r.Date));
                return entity;
            });

            var result = await _holidayRepository.InsertParsedCsvData(recordsMapped);

            if (!result)
                return ResultResponseExtension.Command.InsertFail();

            return ResultResponseExtension.Command.InsertSuccess();
        }
        catch(Exception ex)
        {
            return ResultResponseExtension.Command.InsertFail(ex.Message);
        }
        
    }






}

