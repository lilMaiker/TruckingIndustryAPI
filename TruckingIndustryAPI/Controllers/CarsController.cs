﻿using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.CarsFeatures.Commands;

using TruckingIndustryAPI.Features.CarsFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CarsController(IMediator mediator, ILogger<CarsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _mediator.Send(new GetCarsByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCarsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarsCommand createCarsCommand)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _mediator.Send(createCarsCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarsCommand updateCarsCommand)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _mediator.Send(updateCarsCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCarsCommand deleteCarsCommand)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _mediator.Send(deleteCarsCommand));
        }
    }
}
