using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Cargo : Base.BaseModelLong
    {
        public string NameCargo { get; set; }

        public double WeightCargo { get; set; }

        [ForeignKey("TypeCargo")]
        public long TypeCargoId { get; set; }
        public TypeCargo TypeCargo { get; set; }

        [ForeignKey("Bids")]
        public long BidsId { get; set; }
        public Bid Bids { get; set; }
    }
}
