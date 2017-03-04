namespace ProcessingTools.Services.Web.Services
{
    using System.Threading.Tasks;
    using Contracts.Services;
    using Microsoft.AspNet.Identity;

    public class SmsService : ISmsService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
