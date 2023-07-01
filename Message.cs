using MimeKit;

namespace UmbracoSendGrid
{
	public sealed class Message
	{
		public Message(Dictionary<string, string> to, string subject, string textBody, string htmlBody)
		{
			To = new List<MailboxAddress>();

			To.AddRange(to.Select(x => new MailboxAddress(x.Value, x.Key)));
			Subject = subject;
			TextBody = textBody;
			HtmlBody = htmlBody;
		}
		public Message() { }
		public List<MailboxAddress> To { get; set; }
		public string Subject { get; set; }
		public string TextBody { get; set; }
		public string HtmlBody { get; set; }
	}
}
