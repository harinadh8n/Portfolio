namespace OtpWebApi.Models
{
    public static class TwilioSettings
    {
        public static string AccountSid { get; private set; }
        public static string AuthToken { get; private set; }
        public static string FromPhoneNumber { get; private set; }
        public static string WhatsAppSid { get; private set; }
        public static string WhatsAppAuthToken { get; private set; }
        public static string WhatsAppNumber { get; private set; }

        public static void Initialize(IConfiguration configuration)
        {
            AccountSid = configuration["Twilio:AccountSid"];
            AuthToken = configuration["Twilio:AuthToken"];
            FromPhoneNumber = configuration["Twilio:FromPhoneNumber"];
            WhatsAppSid = configuration["Twilio:WhatsAppSid"];
            WhatsAppAuthToken = configuration["Twilio:WhatsAppAuthToken"];
            WhatsAppNumber = configuration["Twilio:FromWhatsAppNumber"];
        }
    }


}
