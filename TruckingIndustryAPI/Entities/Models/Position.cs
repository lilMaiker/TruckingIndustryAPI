using System.ComponentModel.DataAnnotations;

namespace TruckingIndustryAPI.Entities.Models
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Position : Base.BaseModelLong
    {
        [Display(Name = "Должность")]
        [MaxLength(70, ErrorMessage = "Длина дложности больше 70 символов")]
        [Required(ErrorMessage = "Название должности является обязательным полем")]
        public string NamePosition { get; set; }
    }
}
