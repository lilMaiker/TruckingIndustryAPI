using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.FoundationFeatures.Commands;

using TruckingIndustryAPI.Features.FoundationFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoundationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FoundationController(IMediator mediator, ILogger<FoundationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
        public async Task<IActionResult> Delete([FromQuery] DeleteFoundationCommand deleteFoundationCommand)
        {
            return Ok(await _mediator.Send(deleteFoundationCommand));
        }


    }
}
