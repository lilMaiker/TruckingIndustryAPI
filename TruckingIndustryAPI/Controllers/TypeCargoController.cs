﻿using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.TypeCargoFeatures.Commands;

using TruckingIndustryAPI.Features.TypeCargoFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize("ADMINISTRATOR")]
    public class TypeCargoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TypeCargoController(IMediator mediator, ILogger<TypeCargoController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTypeCargoQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetTypeCargoByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTypeCargoCommand createTypeCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(createTypeCargoCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTypeCargoCommand updateTypeCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(updateTypeCargoCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteTypeCargoCommand deleteTypeCargoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(deleteTypeCargoCommand));
        }
    }
}
