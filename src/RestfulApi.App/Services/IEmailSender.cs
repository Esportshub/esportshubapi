using System.Threading.Tasks;

namespace RestfulApi.App.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
