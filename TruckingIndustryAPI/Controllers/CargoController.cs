using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.CargoFeatures.Commands;

using TruckingIndustryAPI.Features.CargoFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CargoController(IMediator mediator, ILogger<CargoController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetCargoByIdQuery { Id = id }));
        }

        [HttpGet("GetByIdBid/{idBid}")]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetCargoByIdBidQuery { Id = idBid }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCargoQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCargoCommand createCargoCommand)
        {
            /*if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(createCargoCommand);

            if (result == null) return NotFound(new NotFoundResult());

            if (!result.Success) return BadRequest(new BadRequestResult { Errors = result.Errors });

            return Ok(await _mediator.Send(createCargoCommand));*/

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                return Ok(await _mediator.Send(createCargoCommand));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("errors", ex.Message);
                _logger.LogError(ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCargoCommand updateCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(updateCargoCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCargoCommand deleteCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(deleteCargoCommand));
        }
    }
}
