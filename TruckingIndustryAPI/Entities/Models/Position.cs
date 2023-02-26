﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TruckingIndustryAPI.Entities.Models
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Position : Base.BaseModelInt
    {
        [Display(Name = "Должность")]
        [MaxLength(70, ErrorMessage = "Длина дложности больше 70 символов")]
        [Required(ErrorMessage = "Название должности является обязательным полем")]
        public string NamePosition { get; set; }
    }
}
