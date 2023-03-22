using AutoMapper;
using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.DTO.Request;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Features.AccountFeatures.Commands
{
    public class UpdateApplicationRolesCommand : IRequest<ICommandResult>
    {
        public string Id { get; set; }
        public List<RoleSelect> SelectedRoles { get; set; }
        public class UpdateApplicationRolesCommandHandler : IRequestHandler<UpdateApplicationRolesCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateApplicationRolesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateApplicationRolesCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var appUser = await _unitOfWork.UserManager.FindByIdAsync(command.Id);
                    if (appUser == null) return new NotFoundResult() { Data = nameof(ApplicationUser) };

                    var userRoles = await _unitOfWork.UserManager.GetRolesAsync(appUser);

                    var allRoles = _unitOfWork.RoleManager.Roles.ToList();

                    await _unitOfWork.UserManager.RemoveFromRolesAsync(appUser, userRoles);

                    var addedRoles = command.SelectedRoles.Select(s => s.Label);

                    await _unitOfWork.UserManager.AddToRolesAsync(appUser, addedRoles);

                    return new CommandResult() { Data = appUser, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
