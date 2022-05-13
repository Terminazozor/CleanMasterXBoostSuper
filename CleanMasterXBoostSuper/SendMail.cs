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
            Credentials = new System.Net.NetworkCredential("csharpvalentintest@gmail.com", "C#TestValentin");
            DeliveryMethod = SmtpDeliveryMethod.Network;
            EnableSsl = true;
            Host = "smtp.gmail.com";
            Port = 587;
        }
        public void NewMail(string Message,string Destinataire)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("csharpvalentintest@gmail.com");
            mail.To.Add(new MailAddress(Destinataire));
            mail.Body = Message;
            mail.Subject = "test";
            Send(mail);
        }
    }
}
