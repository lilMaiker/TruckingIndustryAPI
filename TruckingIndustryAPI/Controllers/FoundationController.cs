using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.FoundationFeatures.Commands;

using TruckingIndustryAPI.Features.FoundationFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoundationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FoundationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetFoundationByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllFoundationQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFoundationCommand createFoundationCommand)
        {
            return Ok(await _mediator.Send(createFoundationCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFoundationCommand updateFoundationCommand)
        {
            return Ok(await _mediator.Send(updateFoundationCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteFoundationCommand deleteFoundationCommand)
        {
            return Ok(await _mediator.Send(deleteFoundationCommand));
        }


    }
}
