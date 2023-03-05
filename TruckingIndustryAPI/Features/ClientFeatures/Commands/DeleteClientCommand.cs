using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ClientFeatures.Commands
{
    public class DeleteClientCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Client.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Client));
                }
                await _unitOfWork.Client.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
