namespace TruckingIndustryAPI.Services.Email
{
    public interface IEmailSenderService
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
