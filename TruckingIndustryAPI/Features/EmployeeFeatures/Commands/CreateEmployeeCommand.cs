using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Commands
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string Surname { get; set; }
        public string NameEmployee { get; set; }
        public string Patronymic { get; set; }
        public long PositionId { get; set; }
        public string SerialNumber { get; set; }
        public int PassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ApplicationUserId { get; set; }

        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Employee> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
            {
                var employee = _mapper.Map<Employee>(command);
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
                return employee;
            }
        }
    }
}
