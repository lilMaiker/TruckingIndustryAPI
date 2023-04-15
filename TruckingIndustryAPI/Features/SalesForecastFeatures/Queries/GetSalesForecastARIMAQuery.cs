

using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace TruckingIndustryAPI.Features.SalesForecastFeatures.Queries
{
    public class GetSalesForecastARIMAQuery : IRequest<ICommandResult>
    {
        public class GetSalesForecastARIMAQueryHandler : IRequestHandler<GetSalesForecastARIMAQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetSalesForecastARIMAQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetSalesForecastARIMAQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    // 1. Получение данных для обучения модели
                    var bids = await _unitOfWork.Bids.GetAllAsync();
                    var data = bids.Select(b => new SalesData
                    {
                        CarsId = b.CarsId,
                        FoundationId = b.FoundationId,
                        FreightAmount = (float)b.FreightAMount,
                        CurrencyId = b.CurrencyId,
                        StatusId = b.StatusId,
                        EmployeeId = b.EmployeeId,
                        Sales = Convert.ToSingle((float)b.FreightAMount), // преобразование в float
                        Label = Convert.ToSingle((float)b.FreightAMount) // преобразование в float и запись в Label
                    }).ToList();

                    // 2. Создание объекта MLContext
                    var mlContext = new MLContext();

                    var dataView = mlContext.Data.LoadFromEnumerable(data);


                    // 4. Разделение данных на обучающую и тестовую выборки
                    var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
                    var trainData = trainTestSplit.TrainSet;
                    var testData = trainTestSplit.TestSet;

                    // 5. Определение конвейера обработки данных
                    var dataProcessingPipeline = mlContext.Transforms.CopyColumns("Sales", "Label")
                        .Append(mlContext.Transforms.Categorical.OneHotEncoding("CarsId"))
                        .Append(mlContext.Transforms.Categorical.OneHotEncoding("FoundationId"))
                        .Append(mlContext.Transforms.Categorical.OneHotEncoding("CurrencyId"))
                        .Append(mlContext.Transforms.Categorical.OneHotEncoding("StatusId"))
                        .Append(mlContext.Transforms.Categorical.OneHotEncoding("EmployeeId"))
                        .Append(mlContext.Transforms.Concatenate("Features", "CarsId", "FoundationId", "FreightAmount", "CurrencyId", "StatusId", "EmployeeId"))
                        .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                        .Append(mlContext.Transforms.DropColumns("CarsId", "FoundationId", "CurrencyId", "StatusId", "EmployeeId"));

                    // 6. Обучение модели
                    var trainer = mlContext.Regression.Trainers.LbfgsPoissonRegression();
                    var trainingPipeline = dataProcessingPipeline.Append(trainer);
                    var model = trainingPipeline.Fit(trainData);

                    // 7. Предсказание целевой переменной на тестовых данных
                    var predictions = model.Transform(testData);
                    var metrics = mlContext.Regression.Evaluate(predictions);

                    // 8. Использование модели для предсказания на новых данных
                    var engine = mlContext.Model.CreatePredictionEngine<SalesData, SalesPrediction>(model);
                    var newData = new SalesData
                    {
                        CarsId = 1,
                        FoundationId = 2,
                        FreightAmount = 1000,
                        CurrencyId = 3,
                        StatusId = 4,
                        EmployeeId = 5
                    };
                    var prediction = engine.Predict(newData);
                    var forecast = prediction.Prediction;

                    return new CommandResult() { Data = forecast.ToString(), Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
