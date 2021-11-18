using System.Net.Mail;
using System.Net;
using System;
using System.Timers;
using Godot;

namespace Fridge_2_0{
	

	public class Mail{
		public Mail(){
			client = new SmtpClient("smtp.gmail.com"){
				Port = 587,
				Credentials = new NetworkCredential("fridge.2.0.comic@gmail.com","fridgecomic20"),
				EnableSsl = true,
			};
		}


		public Mail(string serviceUrl,int port, string email, string password)
		{
			client = new SmtpClient(serviceUrl){
				Port = port,
				Credentials = new NetworkCredential(email,password),
				EnableSsl = false,
			};
			email_ = email;
		}

		public void sendTestMail(){
			var mailMessage = new MailMessage
			{
				From = new MailAddress("fridge.2.0.comic@gmail.com", "Comic Fridge"),
				Subject = "test",
				Body = "<h1>This is a test</h1>",
				IsBodyHtml =true,
			};
			mailMessage.To.Add("kerv1234@live.fr");
			try{
				client.Send(mailMessage);
			}
			catch (Exception e){
				GD.Print(e);
				throw e;
			}
			
			GD.Print("Sent");
		}
		SmtpClient client;
		string email_;


		public void sendMailToUser(Utilisateur u){
			var mailMessage = new MailMessage
			{
				From = new MailAddress("fridge.2.0.comic@gmail.com", "Comic Fridge"),
				Subject = "Ta dette",
				Body = String.Format("<h3>Yo {0}, t'as une dette de {1}$, et il faudrait que tu la rembourses</h3>.",u.getSurnom(),u.getDette()),
				IsBodyHtml =true,
			};
			mailMessage.To.Add(u.getEmail());
			
			
			client.Send(mailMessage);
		}
	}
}
