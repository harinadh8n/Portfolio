using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace OtpWebApi.Controllers
{
    //Recoverycode for TWILIO G4MAMN8LPVEUGVXAPG7XVJJL
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult SendOtp([FromBody] OtpRequest request)
        {
            // Generate OTP
            string otp = new Random().Next(100000, 999999).ToString();
  
            // Send OTP via SMS
            SendSms(request.Mobile, otp);

            // Return success response
            return Ok(new { Otp = otp }); // Optionally return OTP for testing purposes
        }

        private void SendEmail(string emailAddress, string otp)
        {
            MailMessage mail = new MailMessage("your_email@example.com", emailAddress);
            mail.Subject = "Your OTP Code";
            mail.Body = $"Your OTP is: {otp}";

            SmtpClient smtpClient = new SmtpClient("smtp.your-email-provider.com");
            smtpClient.Port = 587; // or your SMTP port
            smtpClient.Credentials = new System.Net.NetworkCredential("your_email@example.com", "your_password");
            smtpClient.EnableSsl = true;

            smtpClient.Send(mail);
        }

        private void SendSms(string phoneNumber, string otp)
        {
            const string accountSid = "AC63b28e1c7521af09262d4eb03e8a1302"; // Your Twilio account SID
            const string authToken = "1d60efccb22f6ed13b894135f7d96297";   // Your Twilio auth token
            TwilioClient.Init(accountSid, authToken);

            MessageResource.Create(
                body: $"Your OTP is: {otp}",
                from: new Twilio.Types.PhoneNumber("+14152555716"),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );
        }
    }

    public class OtpRequest
    {
        //public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
