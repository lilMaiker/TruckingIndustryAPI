using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;
using TruckingIndustryAPI.Features.CarsFeatures.Commands;
using TruckingIndustryAPI.Features.ClientFeatures.Commands;
using TruckingIndustryAPI.Features.ClientFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ClientsController(IMediator mediator, ILogger<ClientsController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetClientByIdQuery { Id = id }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllClientQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateClientCommand createClientCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(createClientCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateClientCommand updateClientCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(updateClientCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteClientCommand deleteClientCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return HandleResult(await _mediator.Send(deleteClientCommand));
        }
    }
}
