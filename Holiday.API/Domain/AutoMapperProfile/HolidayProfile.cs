using AutoMapper;
using Holiday.API.Common.Extension;
using Holiday.API.Domain.DTO;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Request.Put;
using Holiday.API.Domain.Response;

namespace Holiday.API.Domain.AutoMapperProfile
{
    public class HolidayProfile : Profile
    {
        public HolidayProfile()
        {
            CreateMap<QueryHolidayRequest, HolidayEntity>();
            CreateMap<PostHolidayRequest, HolidayEntity>()
                .ForMember(dest => dest.IsHoliday, opt => opt.MapFrom(src => src.IsHoliday == "是" ? true : false));

            CreateMap<PutHolidayRequest, HolidayEntity>()
                .ForMember(dest => dest.IsHoliday, opt => opt.MapFrom(src => src.IsHoliday == "是" ? true : false)); ;

            CreateMap<HolidayEntity, QueryHolidayResponse>()
                .ForMember(dest => dest.IsHoliday, opt => opt.MapFrom(src => src.IsHoliday == true ? "是" : "否")) 
                .AfterMap((src, dest) => { dest.Date = src.Date.ToString("yyyy-MM-dd"); });

            CreateMap<CsvFileDto, HolidayEntity>()
                .ForMember(src => src.Date, dest => dest.Ignore())
                .ForMember(src => src.IsHoliday, dest => dest.Ignore());

        }
    }
}
