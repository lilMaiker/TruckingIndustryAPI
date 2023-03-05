using System.ComponentModel.DataAnnotations.Schema;

using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Entities.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : Base.BasePersonInfo
    {
        [ForeignKey("Position")]
        public long PositionId { get; set; }
        public Position Position { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
