﻿using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.StatusFeatures.Commands
{
    public class DeleteStatusCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Status.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Status) };
                    await _unitOfWork.Status.DeleteAsync(result.Id);
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
