using System.Net.Mail;
using System.Net;
using System;
using System.Timers;

namespace Fridge_2_0{
    

    public class Mail{
        public Mail(){
            client = new SmtpClient("smtp.gmail.com"){
                Port = 587,
                Credentials = new NetworkCredential(),
                EnableSsl = true,
            };
        }


        public Mail(string serviceUrl,int port, string email, string password)
        {
            client = new SmtpClient(serviceUrl){
                Port = port,
                Credentials = new NetworkCredential(email,password),
                EnableSsl = true,
            };
            email_ = email;
        }

        public void sendTestMail(){
            var mailMessage = new MailMessage
            {
                From = new MailAddress("kerv1234@gmail.com", "Kervin Prinville"),
                Subject = "test",
                Body = "<h1>This is a test</h1>",
                IsBodyHtml =true,
            };
            mailMessage.To.Add("kerv1234@live.fr");

            client.Send(mailMessage);
        }
        SmtpClient client;
        string email_;


        public void sendMailToUser(Utilisateur u){
            var mailMessage = new MailMessage
            {
                From = new MailAddress("kerv1234@gmail.com", "Le Comic"),
                Subject = "Comic Fridge",
                Body = String.Format("<h3>Yo {0}, t'as une dette de {1}$, et il faudrait que tu la rebourses</h3>",u.getSurnom(),u.getDette()),
                IsBodyHtml =true,
            };
            mailMessage.To.Add(u.getEmail());
            
            
            client.Send(mailMessage);
        }
    }
}