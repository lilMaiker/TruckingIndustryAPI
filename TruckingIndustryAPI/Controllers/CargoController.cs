using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.CargoFeatures.Commands;

using TruckingIndustryAPI.Features.CargoFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CargoController(IMediator mediator)
        {
            _mediator = mediator;
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
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                return Ok(await _mediator.Send(createCargoCommand));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("errors", ex.Message);
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
