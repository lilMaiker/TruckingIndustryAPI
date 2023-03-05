using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckingIndustryAPI.Features.BidsFeatures.Commands;

using TruckingIndustryAPI.Features.BidsFeatures.Queries;
using TruckingIndustryAPI.Features.CarsFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BidsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _mediator.Send(new GetBidsByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllBidsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBidsCommand createBidsCommand)
        {
            return Ok(await _mediator.Send(createBidsCommand));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBidsCommand updateBidsCommand)
        {
            return Ok(await _mediator.Send(updateBidsCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteBidsCommand deleteBidsCommand)
        {
            return Ok(await _mediator.Send(deleteBidsCommand));
        }
    }
}
