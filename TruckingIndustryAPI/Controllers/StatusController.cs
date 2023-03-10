using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.StatusFeatures.Commands;

using TruckingIndustryAPI.Features.StatusFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetStatusByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllStatusQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStatusCommand createStatusCommand)
        {
            return Ok(await _mediator.Send(createStatusCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStatusCommand updateStatusCommand)
        {
            return Ok(await _mediator.Send(updateStatusCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteStatusCommand deleteStatusCommand)
        {
            return Ok(await _mediator.Send(deleteStatusCommand));
        }


    }
}
