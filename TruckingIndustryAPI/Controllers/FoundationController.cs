using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Features.ExpensesFeatures.Commands;
using TruckingIndustryAPI.Features.FoundationFeatures.Commands;

using TruckingIndustryAPI.Features.FoundationFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoundationController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FoundationController(IMediator mediator, ILogger<FoundationController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetFoundationByIdQuery { Id = id }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetAllFoundationQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateFoundationCommand createFoundationCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(createFoundationCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateFoundationCommand updateFoundationCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(updateFoundationCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteFoundationCommand deleteFoundationCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(deleteFoundationCommand));
        }


    }
}
