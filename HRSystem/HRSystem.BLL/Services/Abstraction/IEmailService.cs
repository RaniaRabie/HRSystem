namespace HRSystem.BLL.Services.Abstraction
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string body);

    }
}
