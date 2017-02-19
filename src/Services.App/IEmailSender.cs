using System.Threading.Tasks;

namespace Services.App
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
