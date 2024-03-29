﻿using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.CurrencyFeatures.Commands;

using TruckingIndustryAPI.Features.CurrencyFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator, ILogger<CurrencyController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _mediator.Send(new GetCurrencyByIdQuery { Id = id }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCurrencyQuery()));
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyCommand createCurrencyCommand)
        {
            return HandleResult(await _mediator.Send(createCurrencyCommand));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCurrencyCommand updateCurrencyCommand)
        {
            return HandleResult(await _mediator.Send(updateCurrencyCommand));
        }

        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteCurrencyCommand deleteCurrencyCommand)
        {
            return HandleResult(await _mediator.Send(deleteCurrencyCommand));
        }


    }
}
