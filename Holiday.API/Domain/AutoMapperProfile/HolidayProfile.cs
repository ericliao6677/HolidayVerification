using AutoMapper;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;

namespace Holiday.API.Domain.AutoMapperProfile
{
    public class HolidayProfile : Profile
    {
        public HolidayProfile() 
        {
            CreateMap<QueryHolidayRequest, HolidayEntity>();
            CreateMap<PostHolidayRequest, HolidayEntity>();
        }
    }
}
