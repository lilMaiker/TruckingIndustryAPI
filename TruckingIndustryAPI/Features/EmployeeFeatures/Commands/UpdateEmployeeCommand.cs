using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Commands
{
    public class UpdateEmployeeCommand : IRequest<Employee>
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
        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Employee> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Employees.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Employee));
                _mapper.Map(command, result);
                await _unitOfWork.Employees.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
