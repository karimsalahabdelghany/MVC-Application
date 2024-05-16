using System.Net;
using System.Net.Mail;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Demo.PL.Helpers
{
	public static class  EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);           //Mail Server
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("karimabdelghany753@gmail.com", "vmhxisckfuntoken");
			Client.Send("karimabdelghany753@gmail.com", email.To, email.Subject, email.Body);
		}
		
	}
}
