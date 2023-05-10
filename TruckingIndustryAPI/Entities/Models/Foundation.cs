using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Foundation : Base.BaseModelLong
    {
        public string NameFoundation { get; set; }
        public string CertificateNumber { get; set; }
        public string BIC { get; set; }

        [ForeignKey("Client")]
        public long ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Sector")]
        public long SectorId { get; set; }
        public Sector Sector { get; set; }
    }
}
