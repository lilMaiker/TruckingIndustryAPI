using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Entities.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : Base.BaseModelLong
    {
        public string Surname { get; set; }
        public string NameEmployee { get; set; }
        public string Patronymic { get; set; }

        [ForeignKey("Position")]
        public long PositionId { get; set; }
        public Position Position { get; set; }

        public string SerialNumber { get; set; }
        public int PassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
