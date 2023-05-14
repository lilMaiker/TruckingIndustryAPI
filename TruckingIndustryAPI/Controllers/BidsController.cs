using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Features.BidsFeatures.Commands;

using TruckingIndustryAPI.Features.BidsFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BidsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BidsController(IMediator mediator, ILogger<BidsController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            return HandleResult(await _mediator.Send(new GetBidsByIdQuery { Id = id }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _mediator.Send(new GetAllBidsQuery()));
        }

        [HttpGet("ReportAllBids")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReportAllBids()
        {
            return await _mediator.Send(new GetReportAllBidsQuery());
        }

        [HttpGet("ReportCountBidsWithSectors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReportCountBidsWithSectors()
        {
            return await _mediator.Send(new GetReportCountBidsWithSectorsQuery());
        }

        [HttpGet("ReportExpensesInBid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReportExpensesInBid(long bidId)
        {
            return await _mediator.Send(new GetReportExpensesInBidQuery { bidId = bidId });
        }

        [HttpPost("DocumentAct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocumentAct([FromBody] GetDocumentActQuery getDocumentActQuery)
        {
            return await _mediator.Send(getDocumentActQuery);
        }

        [HttpPost("DocumentDog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocumentDog([FromBody] GetDocumentDogQuery getDocumentDogQuery)
        {
            return await _mediator.Send(getDocumentDogQuery);
        }


        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateBidCommand createBidsCommand)
        {
            return HandleResult(await _mediator.Send(createBidsCommand));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateBidCommand updateBidsCommand)
        {
            return HandleResult(await _mediator.Send(updateBidsCommand));
        }

        [HttpDelete]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] DeleteBidCommand deleteBidsCommand)
        {
            return HandleResult(await _mediator.Send(deleteBidsCommand));
        }
    }
}
