using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetReportExpensesInBidQuery : IRequest<FileResult>
    {
        public long bidId { get; set; }
        public class GetReportExpensesInBidQueryHandler : IRequestHandler<GetReportExpensesInBidQuery, FileResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetReportExpensesInBidQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<FileResult> Handle(GetReportExpensesInBidQuery request, CancellationToken cancellationToken)
            {
                var expenses = _unitOfWork.Expenses.GetAllAsync().Result.ToList().Where(e => e.BidsId == request.bidId);

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Expenses");

                    worksheet.Cells[1, 1].Value = "Наименование расхода";
                    worksheet.Cells[1, 2].Value = "Сумма";
                    worksheet.Cells[1, 3].Value = "Валюта";
                    worksheet.Cells[1, 4].Value = "Дата перевода";
                    worksheet.Cells[1, 5].Value = "Комментарий";

                    var row = 2;
                    foreach (var expense in expenses)
                    {
                        worksheet.Cells[row, 1].Value = expense.NameExpense;
                        worksheet.Cells[row, 2].Value = expense.Amount;
                        worksheet.Cells[row, 3].Value = expense.Currency.NameCurrency;
                        worksheet.Cells[row, 4].Value = expense.DateTransfer.HasValue ? expense.DateTransfer.Value.ToString("dd.MM.yyyy") : string.Empty;
                        worksheet.Cells[row, 5].Value = expense.Commnet;

                        row++;
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var fileBytes = package.GetAsByteArray();
                    var fileName = $"expenses_{request.bidId}.xlsx";

                    var file = new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };

                    return file;
                }
            }
        }
    }
}
