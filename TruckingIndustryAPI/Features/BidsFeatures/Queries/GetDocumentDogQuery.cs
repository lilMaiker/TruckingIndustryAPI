using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TruckingIndustryAPI.Configuration.UoW;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetDocumentDogQuery : IRequest<FileResult>
    {
        public long FoundationId { get; set; }
        public string Index { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public DateTime CertNumberDate { get; set; }
        public DateTime CloseDog { get; set; }
        public double Summa { get; set; }
        public string NameBank { get; set; }
        public class GetDocumentDogQueryHandler : IRequestHandler<GetDocumentDogQuery, FileResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetDocumentDogQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<FileResult> Handle(GetDocumentDogQuery request, CancellationToken cancellationToken)
            {
                string templateFilePath = "Templates\\doc_dog.docx";
                string outputFilePath = "dog.docx";

                var foundation = _unitOfWork.Foundation.GetByIdAsync(request.FoundationId).Result;

                Dictionary<string, string> replacements = new Dictionary<string, string>
                {
                    {"summa", $"{request.Summa}"},
                    {"zakazchik", $"{foundation.NameFoundation}"},
                    {"bik", $"{foundation.BIC}"},
                    {"bank", $"{request.NameBank}" },
                    {"ras", "5121527823" },
                    {"date", request.CertNumberDate.ToShortDateString() },
                    {"deist", request.CloseDog.ToShortDateString() },
                    {"svid", $"{foundation.CertificateNumber}" },
                    {"adres", $"{request.Adress}" },
                    {"gorod", $"{request.City}" },
                    {"index", $"{request.Index}" },
                };

                if (File.Exists(outputFilePath)) File.Delete(outputFilePath);
                File.Copy(templateFilePath, outputFilePath, true);

                using (WordprocessingDocument outputDocument = WordprocessingDocument.Open(outputFilePath, true))
                {
                    foreach (var replacement in replacements)
                    {
                        foreach (Text text in outputDocument.MainDocumentPart.Document.Descendants<Text>().Where(t => t.Text.Trim().Equals(replacement.Key, StringComparison.OrdinalIgnoreCase)))
                        {
                            text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                        }
                    }

                    outputDocument.Save();
                    outputDocument.Close();
                }

                byte[] fileBytes = File.ReadAllBytes(outputFilePath);

                var file = new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    FileDownloadName = $"Договор от {DateTime.Now.ToShortDateString()}.docx"
                };

                return file;
            }

        }
    }
}
