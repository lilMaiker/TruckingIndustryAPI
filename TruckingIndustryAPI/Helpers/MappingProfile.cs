using AutoMapper;
using TruckingIndustryAPI.Entities.DTO.Request;
using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
