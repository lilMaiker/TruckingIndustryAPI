using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;

using TruckingIndustryAPI.Features.CargoFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CargoController(IMediator mediator, ILogger<CargoController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetById/{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetCargoByIdQuery { Id = id }));
        }

        [HttpGet("GetByIdBid/{idBid}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdBid(long idBid)
        {
            return Ok(await _mediator.Send(new GetCargoByIdBidQuery { Id = idBid }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCargoQuery()));
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCargoCommand createCargoCommand)
        {
            return HandleResult(await _mediator.Send(createCargoCommand));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCargoCommand updateCargoCommand)
        {
            return HandleResult(await _mediator.Send(updateCargoCommand));
        }

        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteCargoCommand deleteCargoCommand)
        {
            return HandleResult(await _mediator.Send(deleteCargoCommand));
        }
    }
}
