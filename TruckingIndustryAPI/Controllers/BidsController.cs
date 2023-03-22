using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Features.BidsFeatures.Commands;

using TruckingIndustryAPI.Features.BidsFeatures.Queries;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;
using TruckingIndustryAPI.Features.CarsFeatures.Commands;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidsController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public BidsController(IMediator mediator, ILogger<BidsController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetBidsByIdQuery { Id = id }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllBidsQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateBidCommand createBidsCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(createBidsCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateBidCommand updateBidsCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(updateBidsCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteBidCommand deleteBidsCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(deleteBidsCommand));
        }
    }
}
