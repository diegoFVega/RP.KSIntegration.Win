using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Engine.Operations
{
	public class MailOps
	{
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Domain { get; set; }
		public MailAddress From { get; set; }
		public MailAddressCollection To { get; set; }
		public MailAddressCollection Cc { get; set; }
		public MailAddressCollection Bcc { get; set; }
		public string Subject { get; set; }
		public StringBuilder BodyText { get; set; }
		public Encoding BodyEncoding { get; set; }
		public bool IsHtml { get; set; }
		public SortedList<string, string> Attachment { get; set; }

		public MailOps()
		{
			BodyText = new StringBuilder();
			Attachment = new SortedList<string, string>();
		}

		public void SendMail()
		{
			var message = new MailMessage();
			var smtpClient = new SmtpClient();
			object userToken = message;
			smtpClient.Host = SmtpServer;
			smtpClient.Port = SmtpPort;
			smtpClient.EnableSsl = true;
			smtpClient.Credentials = !string.IsNullOrEmpty(Domain) || !string.IsNullOrWhiteSpace(Domain) ? new NetworkCredential(Username, Password, Domain) : new NetworkCredential(Username, Password);
			message.From = From;
			foreach (var mailAddress in To)
				message.To.Add(mailAddress);
			if (Cc != null)
				foreach (var mailAddress in Cc)
					message.CC.Add(mailAddress);
			if (Bcc != null)
				foreach (var mailAddress in Bcc)
					message.Bcc.Add(mailAddress);
			message.Subject = Subject;
			message.Body = BodyText.ToString();
			message.BodyEncoding = BodyEncoding;
			message.IsBodyHtml = IsHtml;
			smtpClient.SendCompleted += SmtpClient_OnCompleted;
			smtpClient.SendAsync(message, userToken);
		}

		private void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
		{
			var subject = ((MailMessage)e.UserState).Subject;
			if (e.Cancelled)
				Console.WriteLine("Send canceled for mail with subject [{0}].", subject);
			if (e.Error != null)
				Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString());
			else
				Console.WriteLine("Message [{0}] sent.", subject);
		}
	}
}