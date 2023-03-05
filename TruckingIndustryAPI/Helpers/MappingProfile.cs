using AutoMapper;
using TruckingIndustryAPI.Entities.DTO.Request;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Features.CarsFeatures.Commands;
using TruckingIndustryAPI.Features.CurrencyFeatures.Commands;
using TruckingIndustryAPI.Features.EmployeeFeatures.Commands;
using TruckingIndustryAPI.Features.PositionFeatures.Commands;
using TruckingIndustryAPI.Features.StatusFeatures.Commands;
using TruckingIndustryAPI.Features.TypeCargoFeatures.Commands;

namespace TruckingIndustryAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<CreatePositionCommand, Position>();
            CreateMap<UpdatePositionCommand, Position>();
            CreateMap<DeletePositionCommand, Position>();

            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            CreateMap<DeleteEmployeeCommand, Employee>();

            CreateMap<CreateStatusCommand, Status>();
            CreateMap<UpdateStatusCommand, Status>();
            CreateMap<DeleteStatusCommand, Status>();

            CreateMap<CreateCurrencyCommand, Currency>();
            CreateMap<UpdateCurrencyCommand, Currency>();
            CreateMap<DeleteCurrencyCommand, Currency>();

            CreateMap<CreateTypeCargoCommand, TypeCargo>();
            CreateMap<UpdateTypeCargoCommand, TypeCargo>();
            CreateMap<DeleteTypeCargoCommand, TypeCargo>();

            CreateMap<CreateCarsCommand, Cars>();
            CreateMap<UpdateCarsCommand, Cars>();
            CreateMap<DeleteCarsCommand, Cars>();
        }
    }
}
