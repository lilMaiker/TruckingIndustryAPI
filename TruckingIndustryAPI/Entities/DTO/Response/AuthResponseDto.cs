namespace TruckingIndustryAPI.Entities.DTO.Response
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? message { get; set; }
        public string? accessToken { get; set; }
        public bool Is2StepVerificationRequired { get; set; }
        public string? Provider { get; set; }
        public string? username { get; set; }
        public string? Email { get; set; }
        public IList<string>? Role { get; set; }
    }
}
