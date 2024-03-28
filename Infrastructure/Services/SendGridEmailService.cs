using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services
{
    public class SendGridEmailService : ISendGridEmailService
    {
        public SendGridEmailService(IOptions<SendGridEmailOptions> options)
        {
            Options = options.Value;
        }

        private SendGridEmailOptions Options { get; }

        public async Task SendAuthCodeAsync(string to, string username, int code)
        {
            var sendGridClient = new SendGridClient(Options.ApiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(Options.SenderEmail, "noreply");
            sendGridMessage.AddTo(to, username);
            sendGridMessage.SetTemplateId("d-a5eecb1a278346d580044b133a0cefbc");
            sendGridMessage.SetTemplateData(new SendGridAuthCodeTemplate()
            {
                UserName = username,
                Code = code
            });
            sendGridMessage.SetClickTracking(false, false);
            sendGridMessage.SetOpenTracking(false);
            sendGridMessage.SetGoogleAnalytics(false);
            sendGridMessage.SetSubscriptionTracking(false);
            await sendGridClient.SendEmailAsync(sendGridMessage);
        }

        public async Task SendResetPasswordEmailAsync(string to, string username, string token)
        {
            var sendGridClient = new SendGridClient(Options.ApiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(Options.SenderEmail, "noreply");
            sendGridMessage.AddTo(to, username);
            sendGridMessage.SetTemplateId("d-8ca9c46155ba48689631210f9952ec01");
            sendGridMessage.SetTemplateData(new SendGridResetTemplate
            {
                UserName = username,
                ResetLink = $"https://www.mindstrade.com/reset?token={token}&email={to}"
            });
            sendGridMessage.SetClickTracking(false, false);
            sendGridMessage.SetOpenTracking(false);
            sendGridMessage.SetGoogleAnalytics(false);
            sendGridMessage.SetSubscriptionTracking(false);
            await sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }

    internal class SendGridResetTemplate
    {
        [JsonProperty("username")] public string UserName { get; set; }

        [JsonProperty("resetlink")] public string ResetLink { get; set; }
    }

    internal class SendGridAuthCodeTemplate
    {
        [JsonProperty("username")] public string UserName { get; set; }

        [JsonProperty("code")] public int Code { get; set; }
    }
}