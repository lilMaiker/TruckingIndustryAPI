using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckingIndustryAPI.Features.CargoFeatures.Commands;

using TruckingIndustryAPI.Features.CargoFeatures.Queries;
using TruckingIndustryAPI.Features.CarsFeatures.Queries;

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
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetCargoByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCargoQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCargoCommand createCargoCommand)
        {
            return Ok(await _mediator.Send(createCargoCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCargoCommand updateCargoCommand)
        {
            return Ok(await _mediator.Send(updateCargoCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCargoCommand deleteCargoCommand)
        {
            return Ok(await _mediator.Send(deleteCargoCommand));
        }
    }
}
