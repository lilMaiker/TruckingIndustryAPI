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
            public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ICommandResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Employees.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Employee) };
                    await _unitOfWork.Employees.DeleteAsync(result.Id);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result.Id, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
