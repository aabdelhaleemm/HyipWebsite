namespace Infrastructure.Configuration
{
    public class SendGridEmailOptions
    {
        public string ApiKey { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }
    }
}