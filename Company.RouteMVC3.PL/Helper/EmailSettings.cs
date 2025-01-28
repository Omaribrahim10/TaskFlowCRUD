using System.Net;
using System.Net.Mail;
using Company.RouteMVC3.DAL.Models;

namespace Company.RouteMVC3.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//Mail Server : gmail.com

			//Smtp

			var client = new SmtpClient("smtp.gmail.com" , 587);

			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("omarimohamed.work@gmail.com", "keoqdtllkbqvtkvf");
			// keoqdtllkbqvtkvf

			client.Send("omarimohamed.work@gmail.com",email.To,email.Subject,email.Body);

		}
	}
}
