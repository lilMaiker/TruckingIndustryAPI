using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckingIndustryAPI.Features.CarsFeatures.Commands;

using TruckingIndustryAPI.Features.CarsFeatures.Queries;
using TruckingIndustryAPI.Features.CurrencyFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetCarsByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCarsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarsCommand createCarsCommand)
        {
            return Ok(await _mediator.Send(createCarsCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarsCommand updateCarsCommand)
        {
            return Ok(await _mediator.Send(updateCarsCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCarsCommand deleteCarsCommand)
        {
            return Ok(await _mediator.Send(deleteCarsCommand));
        }
    }
}
