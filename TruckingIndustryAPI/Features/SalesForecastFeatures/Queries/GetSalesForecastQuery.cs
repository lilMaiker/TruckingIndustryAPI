using MediatR;

using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Features.BidsFeatures.Queries;

using static Microsoft.ML.ForecastingCatalog;

namespace TruckingIndustryAPI.Features.SalesForecastFeatures.Queries
{
    public class GetSalesForecastQuery : IRequest<ICommandResult>
    {
        public int numMonths { get; set; }
        public class GetSalesForecastQueryHandler : IRequestHandler<GetSalesForecastQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetSalesForecastQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetSalesForecastQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var groupedBids = await _unitOfWork.Bids.GetAllAsync();
                    var sales = groupedBids.Where(b => b.PayDate.HasValue)
                        .GroupBy(b => new { Year = b.PayDate.Value.Year, Month = b.PayDate.Value.Month })
                        .Select(g => new { g.Key.Year, g.Key.Month, FreightSum = g.Sum(b => b.FreightAMount) })
                        .OrderBy(g => g.Year)
                        .ThenBy(g => g.Month)
                        .ToList();

                    double alpha = 0.1;
                    double initialForecast = sales.First().FreightSum;
                    double initialSales = sales.Skip(1).First().FreightSum;

                    List<object> forecastResults = new();

                    foreach (var sale in sales)
                    {
                        double forecast = alpha * sale.FreightSum + (1 - alpha) * initialForecast;
                        initialForecast = forecast;
                        var realMonth = sale.Month + sales.Count;

                        forecastResults.Add(new { sale.Year, realMonth, Forecast = forecast });
                    }

                    return new CommandResult() { Data = forecastResults, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }

            private double ExponentialSmoothingForecast(List<double> salesHistory, double lastForecast, double alpha)
            {
                double currentSale = salesHistory.Count > 0 ? salesHistory.Last() : lastForecast;
                double forecast = alpha * currentSale + (1 - alpha) * lastForecast;

                return forecast;
            }
        }
    }
}
