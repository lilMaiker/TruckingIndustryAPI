using System.ComponentModel;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Cars : Base.BaseModelLong
    {
        public string BrandTrailer { get; set; }
        public string TrailerNumber { get; set; }
        public DateTime LastDateTechnicalInspection { get; set; }
        public double MaxWeight { get; set; }
        [DisplayName("Открытый борт")]
        public bool WithOpenSide { get; set; }
        [DisplayName("Холодильник")]
        public bool WithRefrigerator { get; set; }
        [DisplayName("Тент")]
        public bool WithTent { get; set; }
        [DisplayName("Гидроборт")]
        public bool WithHydroboard { get; set; }

    }
}
