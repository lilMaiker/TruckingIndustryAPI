using AutoMapper;

using TruckingIndustryAPI.Entities.DTO.Request;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Features.BidsFeatures.Commands;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;
using TruckingIndustryAPI.Features.CarsFeatures.Commands;
using TruckingIndustryAPI.Features.ClientFeatures.Commands;
using TruckingIndustryAPI.Features.CurrencyFeatures.Commands;
using TruckingIndustryAPI.Features.EmployeeFeatures.Commands;
using TruckingIndustryAPI.Features.ExpensesFeatures.Commands;
using TruckingIndustryAPI.Features.FoundationFeatures.Commands;
using TruckingIndustryAPI.Features.PositionFeatures.Commands;
using TruckingIndustryAPI.Features.Routes.Commands;
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

            CreateMap<CreateCarCommand, Car>();
            CreateMap<UpdateCarCommand, Car>();
            CreateMap<DeleteCarCommand, Car>();

            CreateMap<CreateClientCommand, Client>();
            CreateMap<UpdateClientCommand, Client>();
            CreateMap<DeleteClientCommand, Client>();

            CreateMap<CreateFoundationCommand, Foundation>();
            CreateMap<UpdateFoundationCommand, Foundation>();
            CreateMap<DeleteFoundationCommand, Foundation>();

            CreateMap<CreateBidsCommand, Bid>();
            CreateMap<UpdateBidsCommand, Bid>();
            CreateMap<DeleteBidsCommand, Bid>();

            CreateMap<CreateCargoCommand, Cargo>();
            CreateMap<UpdateCargoCommand, Cargo>();
            CreateMap<DeleteCargoCommand, Cargo>();

            CreateMap<CreateExpensesCommand, Expense>();
            CreateMap<UpdateExpensesCommand, Expense>();
            CreateMap<DeleteExpensesCommand, Expense>();

            CreateMap<CreateRouteCommand, Entities.Models.Route>();
            CreateMap<UpdateRouteCommand, Entities.Models.Route>();
            CreateMap<DeleteRouteCommand, Entities.Models.Route>();

        }
    }
}
