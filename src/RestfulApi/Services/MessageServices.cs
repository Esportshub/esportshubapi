using System.Threading.Tasks;

namespace RestfulApi.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //@TODO: Implement email service
            return Task.FromResult(0);
        }
    }
}
