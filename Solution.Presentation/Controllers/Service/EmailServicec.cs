using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Solution.Presentation.Controllers.Service
{
    public class EmailServicec:IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var credentialUserName = "mouhamedsaid.raoudh@esprit.tn";
            var sentFrom = "mouhamedsaid.raoudh@esprit.tn";
            var pwd = "Bb123456";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.gmail.com");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send:
            return client.SendMailAsync(mail);
            }

        //public ActionResult SendMail()
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("ConsomiTounsi", "mouhamedsaid.raoudh@esprit.tn"));
        //    message.To.Add(new MailboxAddress("said", "email"));
        //    message.Subject = "just a subjust";
        //    message.Body = new TextPart("plain")
        //    {
        //        Text = "Iam using mailkit nuget"
        //};
        //using (var client=new SmtpClient()){
        //        client.Connect("smtp.gmail.com",587,false);
        //        client.Authenticate("mouhamedsaid.raoudh@esprit.tn", "Bb123456");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //        }
    }
}