using Microsoft.AspNetCore.Mvc;
using OtpWebApi.Models;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace OtpWebApi.Controllers
{
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
            //SendSms(request.Mobile, otp);
            SendWhatsApp(request.Mobile, otp);
            // Return success response
            return Ok(new { Otp = otp }); // Optionally return OTP for testing purposes
        }
        private void SendSms(string phoneNumber, string otp)
        {
            TwilioClient.Init(TwilioSettings.AccountSid, TwilioSettings.AuthToken);

            MessageResource.Create(
                body: $"Your OTP is: {otp} Please use it in under 5 Min",
                from: new Twilio.Types.PhoneNumber(TwilioSettings.FromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );
        }
        private void SendWhatsApp(string phoneNumber, string otp)
        {
            // Initialize Twilio client with Account SID and Auth Token
            TwilioClient.Init(TwilioSettings.WhatsAppSid, TwilioSettings.WhatsAppAuthToken);

            try
            {
                // Create and send the message
                var message = MessageResource.Create(
                    body: $"Your OTP is: {otp} Please use it in under 5 Min",
                    from: new Twilio.Types.PhoneNumber($"whatsapp:{TwilioSettings.WhatsAppNumber}"), // Ensure this is correct
                    to: new Twilio.Types.PhoneNumber($"whatsapp:{phoneNumber}") // Ensure recipient format is correct
                );
            }
            catch (Twilio.Exceptions.ApiException ex)
            {
                // Handle exceptions and log error messages
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public class OtpRequest
    {
        //public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
