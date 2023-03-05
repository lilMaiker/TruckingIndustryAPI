using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.EmployeeFeatures.Queries
{
    public class GetAllEmployeeQuery : IRequest<IEnumerable<Employee>>
    {
        public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IEnumerable<Employee>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllEmployeeQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Employee>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Employees.GetAllAsync();
            }
        }
    }
}
