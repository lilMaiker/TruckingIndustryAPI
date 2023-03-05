using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ClientFeatures.Commands
{
    public class UpdateClientCommand : IRequest<Client>
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string SerialNumber { get; set; }
        public int PassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Client> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Client.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Client));
                _mapper.Map(command, result);
                await _unitOfWork.Client.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
