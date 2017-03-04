namespace ProcessingTools.Services.Web.Services
{
    using System.Threading.Tasks;
    using Contracts.Services;
    using Microsoft.AspNet.Identity;

    public class EmailService : IEmailService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
