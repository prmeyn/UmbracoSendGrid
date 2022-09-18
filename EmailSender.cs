using Hangfire;
using MailKit.Net.Smtp;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace UmbracoSendGrid
{
	public static class EmailSender
	{
		private static bool IsLocal;
		private static SMTPsettings _SMTPsettings;

		public static void Initialize(bool isLocal, SMTPsettings SMTPsettings)
		{
			IsLocal = isLocal;
			_SMTPsettings = SMTPsettings;
		}

		public static void SendEmail(Dictionary<string, string> toEmailsAndName, string subject, string textBody, string htmlBody)
		{
			if (_SMTPsettings != null)
			{
				if (IsLocal)
				{
					CreateEmailMessageAndSend(toEmailsAndName, subject, textBody, htmlBody);
				}
				else
				{
					BackgroundJob.Enqueue(() => CreateEmailMessageAndSend(toEmailsAndName, subject, textBody, htmlBody));
				}
			}
		}
		public static void SendEmail(Dictionary<string, string> toEmailsAndName, KeyValuePair<string, Dictionary<string, string>> templateIdAndTemplate)
		{
			if (_SMTPsettings != null)
			{
				if (!string.IsNullOrEmpty(templateIdAndTemplate.Key) && toEmailsAndName.Any())
				{
					var fromEmail = new EmailAddress(_SMTPsettings.From, _SMTPsettings.FromName);
					var toEmails = toEmailsAndName.Select(toEmailObj => new EmailAddress(toEmailObj.Key, toEmailObj.Value)).ToList();
					var sendGridMessage = MailHelper.CreateSingleTemplateEmail(
							from: fromEmail,
							to: toEmails.First(),
							templateIdAndTemplate.Key,
							templateIdAndTemplate.Value
						);
					sendGridMessage.SetReplyTo(fromEmail);
					if (toEmails.Count > 1)
					{
						var ccs = toEmails.GetRange(1, toEmails.Count - 1);
						sendGridMessage.AddCcs(ccs);
					}
					if (IsLocal)
					{
						SendEmailMessageViaAPI(sendGridMessage);
					}
					else
					{
						BackgroundJob.Enqueue(() => SendEmailMessageViaAPI(sendGridMessage));
					}
					
				}
			}
		}

		public static void SendEmailMessageViaAPI(SendGridMessage sendGridMessage)
		{
			var sendGridClient = new SendGridClient(_SMTPsettings.Password);
			var response = sendGridClient.SendEmailAsync(sendGridMessage).Result;
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception(response.StatusCode.ToString());
			}
		}

		public static void CreateEmailMessageAndSend(Dictionary<string, string> toEmailsAndName, string subject, string textBody, string htmlBody)
		{

			Message message = new(toEmailsAndName, subject, textBody, htmlBody);
			MimeMessage emailMessage = new();
			emailMessage.From.Add(new MailboxAddress(_SMTPsettings.FromName, _SMTPsettings.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			var bodyBuilder = new BodyBuilder();
			bodyBuilder.TextBody = message.TextBody;
			bodyBuilder.HtmlBody = message.HtmlBody;
			emailMessage.Body = bodyBuilder.ToMessageBody();
			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect(
						host: _SMTPsettings.Host,
						port: _SMTPsettings.Port,
						options: _SMTPsettings.SecureSocketOptions);

					client.Authenticate(
						userName: _SMTPsettings.Username,
						password: _SMTPsettings.Password);
					client.Send(emailMessage);
				}
				catch
				{
					//log an error message or throw an exception or both.
					throw;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
	}
}