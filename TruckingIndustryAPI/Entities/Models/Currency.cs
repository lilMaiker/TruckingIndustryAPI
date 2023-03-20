using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Currency : Base.BaseModelLong
    {
        [DisplayName("Валюта")]
        [Required]
        public string? NameCurrency { get; set; }

        [DisplayName("Код валюты")]
        [MaxLength(3)]
        [Required]
        public string? CurrencyCode { get; set; }
    }
}
