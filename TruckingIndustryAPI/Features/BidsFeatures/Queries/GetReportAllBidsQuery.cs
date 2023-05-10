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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var bids = _unitOfWork.Bids.GetAllAsync().Result.ToList();

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Bids");

                    worksheet.Cells[1, 1].Value = "CarsId";
                    worksheet.Cells[1, 2].Value = "FoundationId";
                    worksheet.Cells[1, 3].Value = "FreightAmount";
                    worksheet.Cells[1, 4].Value = "CurrencyId";
                    worksheet.Cells[1, 5].Value = "DateToLoad";
                    worksheet.Cells[1, 6].Value = "DateToUnload";
                    worksheet.Cells[1, 7].Value = "ActAccNumber";
                    worksheet.Cells[1, 8].Value = "StatusId";
                    worksheet.Cells[1, 9].Value = "PayDate";
                    worksheet.Cells[1, 10].Value = "EmployeeId";

                    for (int i = 0; i < bids.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = bids[i].CarsId;
                        worksheet.Cells[i + 2, 2].Value = bids[i].FoundationId;
                        worksheet.Cells[i + 2, 3].Value = bids[i].FreightAMount;
                        worksheet.Cells[i + 2, 4].Value = bids[i].CurrencyId;
                        worksheet.Cells[i + 2, 5].Value = bids[i].DateToLoad;
                        worksheet.Cells[i + 2, 5].Style.Numberformat.Format = "dd.MM.yyyy";
                        worksheet.Cells[i + 2, 6].Value = bids[i].DateToUnload;
                        worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "dd.MM.yyyy";
                        worksheet.Cells[i + 2, 7].Value = bids[i].ActAccNumber;
                        worksheet.Cells[i + 2, 8].Value = bids[i].StatusId;
                        worksheet.Cells[i + 2, 9].Value = bids[i].PayDate;
                        worksheet.Cells[i + 2, 9].Style.Numberformat.Format = "dd.MM.yyyy";
                        worksheet.Cells[i + 2, 10].Value = bids[i].EmployeeId;
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
