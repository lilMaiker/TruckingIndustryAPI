using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Commands
{
    public class DeleteEmployeeCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Employees.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Employee));
                }
                await _unitOfWork.Employees.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
