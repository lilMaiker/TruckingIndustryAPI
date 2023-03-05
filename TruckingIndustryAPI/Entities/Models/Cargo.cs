using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Cargo : Base.BaseModelLong
    {
        public string NameCargo { get;set;}

        [ForeignKey("TypeCargo")]
        public long TypeCargoId { get; set; }
        public TypeCargo TypeCargo { get; set; }

        //Заявки
    }
}
