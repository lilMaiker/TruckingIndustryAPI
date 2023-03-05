using System.ComponentModel;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Status : Base.BaseModelLong
    {
        [DisplayName("Статус")]
        public string NameStatus { get; set; }
    }
}
