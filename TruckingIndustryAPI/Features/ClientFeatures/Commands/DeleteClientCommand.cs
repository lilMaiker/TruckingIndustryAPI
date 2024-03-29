﻿using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.ClientFeatures.Commands
{
    public class DeleteClientCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteClientCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ICommandResult> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Client.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Client) };
                    await _unitOfWork.Client.DeleteAsync(result.Id);
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
