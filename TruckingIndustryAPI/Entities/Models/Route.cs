using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Route : Base.BaseModelLong
    {
        public string PointA { get; set; }
        public string PointB { get; set; }
        [ForeignKey("Bids")]
        public long BidsId { get; set; }
        public Bids Bids { get; set; }
    }
}
