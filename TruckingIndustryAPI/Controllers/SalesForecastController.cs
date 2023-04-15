using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Entities.Controller;
using TruckingIndustryAPI.Features.SalesForecastFeatures.Queries;

namespace TruckingIndustryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesForecastController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SalesForecastController(IMediator mediator, ILogger<BidsController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSalesForecast()
        {
            return HandleResult(await _mediator.Send(new GetSalesForecastARIMAQuery()));
            //return HandleResult(await _mediator.Send(new GetSalesForecastQuery { numMonths = numMonths }));
        }
    }
}
