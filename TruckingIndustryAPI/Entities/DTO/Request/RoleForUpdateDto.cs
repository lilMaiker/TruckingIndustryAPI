using System.Data;

namespace TruckingIndustryAPI.Entities.DTO.Request
{
    public class RoleForUpdateDto
    {
        public string Id { get; set; }
        public List<RoleSelect> SelectedRoles { get; set; }
    }
}
