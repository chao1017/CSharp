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
            var smtpClient = new SmtpClient("csmail2.tradevan.com.tw")
            {
                Port = 25,
                Credentials = new NetworkCredential("", ""),
                EnableSsl = false,
            };

            //smtpClient.Send("rogerchao@172.20.23.244", "chaoshuangjui@gmail.com", "Test主題", "12345678");
            smtpClient.Send("service.utm@tradevan.com.tw", "chaoshuangjui@gmail.com", "Test從驗證區寄信", "驗證區寄到我的信箱");
        }
    }
}
