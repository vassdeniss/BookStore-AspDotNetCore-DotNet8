using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookStore.Common
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // TODO:
            return Task.CompletedTask;
        }
    }
}
