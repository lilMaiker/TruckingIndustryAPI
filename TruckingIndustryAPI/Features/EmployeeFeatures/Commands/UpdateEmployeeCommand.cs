using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Commands
{
    public class UpdateEmployeeCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public long PositionId { get; set; }
        public string SerialNumber { get; set; }
        public int PassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ApplicationUserId { get; set; }
        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Employees.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Employee) };
                    _mapper.Map(command, result);
                    await _unitOfWork.Employees.UpdateAsync(result);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
