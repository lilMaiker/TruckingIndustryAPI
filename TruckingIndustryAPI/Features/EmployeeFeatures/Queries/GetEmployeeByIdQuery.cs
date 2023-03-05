using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Queries
{
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public long Id { get; set; }
        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Employees.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Employee));
                return result;
            }
        }
    }
}
