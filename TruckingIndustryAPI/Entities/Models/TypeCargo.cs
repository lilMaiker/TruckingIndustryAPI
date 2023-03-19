using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TruckingIndustryAPI.Entities.Models
{
    /// <summary>
    /// Тип груза
    /// </summary>
    public class TypeCargo : Base.BaseModelLong
    {
        [DisplayName("Тип груза")]
        [MaxLength(150)]
        public string NameTypeCargo { get; set; }
    }
}
