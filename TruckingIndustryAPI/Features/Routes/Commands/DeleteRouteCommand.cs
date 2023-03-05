using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.Routes.Commands
{
    public class DeleteRouteCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteRouteCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Route.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Entities.Models.Route));
                }
                await _unitOfWork.Route.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
