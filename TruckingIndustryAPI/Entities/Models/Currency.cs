using System.ComponentModel;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Currency : Base.BaseModelLong
    {
        [DisplayName("Валюта")]
        public string NameCurrency { get; set; }
        [DisplayName("Код валюты")]
        public string CurrencyCode { get; set; }
    }
}
