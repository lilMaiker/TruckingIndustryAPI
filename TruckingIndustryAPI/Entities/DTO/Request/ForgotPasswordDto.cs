using System.ComponentModel.DataAnnotations;

namespace TruckingIndustryAPI.Entities.DTO.Request
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? ClientURI { get; set; }
    }
}
