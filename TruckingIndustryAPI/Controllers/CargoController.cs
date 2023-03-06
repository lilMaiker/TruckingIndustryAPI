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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery]long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetCargoByIdQuery { Id = id }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBid([FromQuery] long idBid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetCargoByIdQuery { Id = idBid }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCargoQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCargoCommand createCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(createCargoCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCargoCommand updateCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(updateCargoCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCargoCommand deleteCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(deleteCargoCommand));
        }
    }
}
