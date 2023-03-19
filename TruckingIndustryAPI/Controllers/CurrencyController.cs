using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Features.CurrencyFeatures.Commands;

using TruckingIndustryAPI.Features.CurrencyFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetCurrencyByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCurrencyQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCurrencyCommand createCurrencyCommand)
        {
            return Ok(await _mediator.Send(createCurrencyCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCurrencyCommand updateCurrencyCommand)
        {
            return Ok(await _mediator.Send(updateCurrencyCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCurrencyCommand deleteCurrencyCommand)
        {
            return Ok(await _mediator.Send(deleteCurrencyCommand));
        }


    }
}
