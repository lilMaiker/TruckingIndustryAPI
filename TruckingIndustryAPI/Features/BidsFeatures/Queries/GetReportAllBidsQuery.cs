using MediatR;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using BadRequestResult = TruckingIndustryAPI.Entities.Command.BadRequestResult;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetReportAllBidsQuery : IRequest<FileResult>
    {
        public class GetReportAllBidsQueryHandler : IRequestHandler<GetReportAllBidsQuery, FileResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetReportAllBidsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<FileResult> Handle(GetReportAllBidsQuery request, CancellationToken cancellationToken)
            {
                var bids = _unitOfWork.Bids.GetAllAsync().Result.ToList();

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Bids");

                    worksheet.Cells[1, 1].Value = "Машина";
                    worksheet.Cells[1, 2].Value = "Организация";
                    worksheet.Cells[1, 3].Value = "Сумма фрахта";
                    worksheet.Cells[1, 4].Value = "Валюта";
                    worksheet.Cells[1, 5].Value = "Дата загрузки";
                    worksheet.Cells[1, 6].Value = "Дата разгрузки";
                    worksheet.Cells[1, 7].Value = "Номер акта";
                    worksheet.Cells[1, 8].Value = "Статус";
                    worksheet.Cells[1, 9].Value = "Дата оплаты";
                    worksheet.Cells[1, 10].Value = "Сотрудник";

                    for (int i = 0; i < bids.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = bids[i].Cars.TrailerNumber;
                        worksheet.Cells[i + 2, 2].Value = bids[i].Foundation.NameFoundation;
                        worksheet.Cells[i + 2, 3].Value = bids[i].FreightAMount;
                        worksheet.Cells[i + 2, 4].Value = bids[i].Currency.NameCurrency;
                        worksheet.Cells[i + 2, 5].Value = bids[i].DateToLoad;
                        worksheet.Cells[i + 2, 5].Style.Numberformat.Format = "dd.MM.yyyy";
                        worksheet.Cells[i + 2, 6].Value = bids[i].DateToUnload;
                        worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "dd.MM.yyyy";
                        worksheet.Cells[i + 2, 7].Value = bids[i].ActAccNumber;
                        worksheet.Cells[i + 2, 8].Value = bids[i].Status.NameStatus;
                        worksheet.Cells[i + 2, 9].Value = bids[i].PayDate;
                        worksheet.Cells[i + 2, 9].Style.Numberformat.Format = "dd.MM.yyyy";
                        worksheet.Cells[i + 2, 10].Value = $"{bids[i].Employee.Surname} {bids[i].Employee.Name} {bids[i].Employee.Patronymic}";
                    }

                    var fileBytes = package.GetAsByteArray();
                    var fileName = "bids.xlsx";

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
