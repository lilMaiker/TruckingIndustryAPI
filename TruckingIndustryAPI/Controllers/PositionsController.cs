using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.PositionFeatures.Commands;
using TruckingIndustryAPI.Features.PositionFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PositionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PositionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionCommand createPositionCommand)
        {
            return Ok(await _mediator.Send(createPositionCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePositionCommand updatePositionCommand)
        {
            return Ok(await _mediator.Send(updatePositionCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeletePositionCommand deletePositionCommand)
        {
            return Ok(await _mediator.Send(deletePositionCommand));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPositionsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetPositionByIdQuery { Id = id }));
        }
    }
}
