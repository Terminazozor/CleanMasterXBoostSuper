using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CleanMasterXBoostSuper
{
    internal class SendMail : SmtpClient
    {
        public SendMail()
        {
            UseDefaultCredentials = true;
            Credentials = new System.Net.NetworkCredential("csharpvalentintest@hotmail.com", "C#TestValentin");
            DeliveryMethod = SmtpDeliveryMethod.Network;
            EnableSsl = true;
            Host = "smtp-mail.outlook.com";
            Port = 587;
            
        }
        public void NewMail(string Message,string Destinataire)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("csharpvalentintest@hotmail.com");
            mail.To.Add(new MailAddress(Destinataire));
            mail.Body = Message;
            mail.Subject = "test";
            Send(mail);
        }
    }
}
