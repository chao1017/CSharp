using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleSendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            mail_send();
            Console.WriteLine("Mail Sent Successfully!");
            Console.ReadLine();
        }

        private static void mail_send()
        {
            var smtpClient = new SmtpClient("host")
            {
                Port = 587,
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = false,
            };

            smtpClient.Send("email", "recipient", "subject", "body");
        }
    }
}
