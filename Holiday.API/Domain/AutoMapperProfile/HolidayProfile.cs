using AutoMapper;
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
            CreateMap<PostHolidayRequest, HolidayEntity>();
            CreateMap<PutHolidayRequest, HolidayEntity>();
            CreateMap<HolidayEntity, QueryHolidayResponse>()
                .AfterMap((src, dest) => { dest.Date = src.Date.ToString("yyyy/MM/dd");});
        }
    }
}
