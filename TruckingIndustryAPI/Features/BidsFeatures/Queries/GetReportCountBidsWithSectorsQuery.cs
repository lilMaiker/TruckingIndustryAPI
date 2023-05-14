using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TruckingIndustryAPI.Configuration.UoW;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetReportCountBidsWithSectorsQuery : IRequest<FileResult>
    {
        public class GetReportCountBidsWithSectorsQueryHandler : IRequestHandler<GetReportCountBidsWithSectorsQuery, FileResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetReportCountBidsWithSectorsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<FileResult> Handle(GetReportCountBidsWithSectorsQuery request, CancellationToken cancellationToken)
            {
                var bids = _unitOfWork.Bids.GetAllAsync().Result.ToList();

                var groupedBids = bids
                    .GroupBy(b => b.Foundation?.Sector?.NameSector) 
                    .Select(g => new { SectorName = g.Key, Count = g.Count() }) 
                    .OrderBy(g => g.SectorName); 

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Bids by Sector");

                    worksheet.Cells[1, 1].Value = "Сектор";
                    worksheet.Cells[1, 2].Value = "Кол-во заявок";

                    worksheet.Column(1).Width = 45; 
                    worksheet.Column(2).Width = 15; 

                    var row = 2;
                    foreach (var group in groupedBids)
                    {
                        worksheet.Cells[row, 1].Value = group.SectorName;
                        worksheet.Cells[row, 2].Value = group.Count;

                        row++;
                    }

                    var fileBytes = package.GetAsByteArray();
                    var fileName = "bids_by_sector.xlsx";

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
