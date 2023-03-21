using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Commands
{
    public class DeleteEmployeeCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Employees.GetByIdAsync(command.Id);
                if (result == null) return new NotFoundResult() { };
                await _unitOfWork.Employees.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return new CommandResult() {Data = result.Id, Errors = null, Success = true };
            }
        }
    }
}
