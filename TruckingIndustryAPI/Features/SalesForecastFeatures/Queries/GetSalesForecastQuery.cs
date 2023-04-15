using MediatR;

using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Features.BidsFeatures.Queries;

namespace TruckingIndustryAPI.Features.SalesForecastFeatures.Queries
{
    public class GetSalesForecastQuery : IRequest<ICommandResult>
    {
        public int? numMonths { get; set; }
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
                    var numMonths = request.numMonths;

                    if (!numMonths.HasValue) numMonths = 3;

                    // Группируем заявки по году и месяцу
                    var groupedBids = await _unitOfWork.Bids.GetAllAsync();
                    var sales = groupedBids.Where(b => b.PayDate.HasValue)
                        .GroupBy(b => new { Year = b.PayDate.Value.Year, Month = b.PayDate.Value.Month })
                        .Select(g => new { g.Key.Year, g.Key.Month, FreightSum = g.Sum(b => b.FreightAMount) })
                        .OrderBy(g => g.Year)
                        .ThenBy(g => g.Month)
                        .ToList();

                    // Если у нас нет исходных данных, то возвращаем пустую коллекцию
                    if (sales.Count == 0) return new CommandResult() { Data = new List<Forecast>(), Success = false };

                    // Находим последний месяц в исходных данных
                    var lastYear = sales.Max(g => g.Year);
                    var lastMonthNum = sales.Where(g => g.Year == lastYear).Max(g => g.Month);

                    // Рассчитываем прогноз на каждый месяц вперед
                    var forecasts = new List<Forecast>();
                    double lastForecast = Convert.ToDouble(sales.Sum(s => Convert.ToDouble(s.FreightSum))) / Convert.ToDouble(sales.Count);

                    double alpha = 0.1; // Коэффициент сглаживания
                    List<double> salesHistory = new List<double>(); // История продаж
                    for (int i = 0; i < numMonths; i++)
                    {
                        // Находим последний месяц прогноза
                        int forecastMonth = lastMonthNum + i + 1;
                        int forecastYear = lastYear;
                        if (forecastMonth > 12)
                        {
                            forecastMonth -= 12;
                            forecastYear += 1;
                        }

                        // Рассчитываем прогноз на следующий месяц
                        var lastSale = sales.LastOrDefault(s => s.Year == lastYear && s.Month == lastMonthNum + i);
                        if (lastSale != null)
                        {
                            salesHistory.Add(lastSale.FreightSum);
                        }
                        double forecast = ExponentialSmoothingForecast(salesHistory, lastForecast, alpha);

                        // Добавляем прогноз в коллекцию
                        forecasts.Add(new Forecast { Year = forecastYear, Month = forecastMonth, FreightSum = forecast });

                        // Обновляем значения для следующей итерации
                        lastForecast = forecast;
                    }

                    // Возвращаем результат пользователю
                    return new CommandResult() { Data = forecasts, Success = true };

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
