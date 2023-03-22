using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.TypeCargoFeatures.Commands;

using TruckingIndustryAPI.Features.TypeCargoFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeCargoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public TypeCargoController(IMediator mediator, ILogger<TypeCargoController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTypeCargoQuery()));
        }

        [HttpGet("{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetTypeCargoByIdQuery { Id = id }));
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateTypeCargoCommand createTypeCargoCommand)
        {
            return HandleResult(await _mediator.Send(createTypeCargoCommand));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateTypeCargoCommand updateTypeCargoCommand)
        {
            return HandleResult(await _mediator.Send(updateTypeCargoCommand));
        }

        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteTypeCargoCommand deleteTypeCargoCommand)
        {
            return HandleResult(await _mediator.Send(deleteTypeCargoCommand));
        }
    }
}
