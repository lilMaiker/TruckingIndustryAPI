using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Route : Base.BaseModelLong
    {
        [DisplayName("Пункт А")]
        [MaxLength(200)]
        public string PointA { get; set; }

        [DisplayName("Пункт Б")]
        [MaxLength(200)]
        public string PointB { get; set; }
        [ForeignKey("Bids")]
        public long BidsId { get; set; }
        public Bids Bids { get; set; }
    }
}
