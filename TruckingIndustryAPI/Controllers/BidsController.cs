using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.BidsFeatures.Commands;

using TruckingIndustryAPI.Features.BidsFeatures.Queries;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;
using TruckingIndustryAPI.Features.CarsFeatures.Commands;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public BidsController(IMediator mediator, ILogger<BidsController> logger)
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
        public async Task<IActionResult> Create(CreateBidsCommand createBidsCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(createBidsCommand);

            if (!result.Success)
            {
                _logger.LogError(result.Errors);
                return BadRequest(new Entities.Command.BadRequestResult { Errors = result.Errors });
            }

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateBidsCommand updateBidsCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(updateBidsCommand);

            if (!result.Success && result.Errors.Contains("Not Found")) return NotFound();

            if (!result.Success)
            {
                _logger.LogError(result.Errors);
                return BadRequest(new Entities.Command.BadRequestResult { Errors = result.Errors });
            }

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteBidsCommand deleteBidsCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(deleteBidsCommand);

            if (!result.Success && result.Errors.Contains("Not Found")) return NotFound();

            if (!result.Success)
            {
                _logger.LogError(result.Errors);
                return BadRequest(new Entities.Command.BadRequestResult { Errors = result.Errors });
            }

            return Ok(result);
        }
    }
}
