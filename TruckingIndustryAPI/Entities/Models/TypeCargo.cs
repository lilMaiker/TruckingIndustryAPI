using System.ComponentModel;

namespace TruckingIndustryAPI.Entities.Models
{
    /// <summary>
    /// Тип груза
    /// </summary>
    public class TypeCargo : Base.BaseModelLong
    {
        [DisplayName("Тип груза")]
        public string NameTypeCargo { get; set; }
    }
}
