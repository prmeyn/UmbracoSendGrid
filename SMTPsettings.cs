using MailKit.Security;

namespace UmbracoSendGrid
{
	public class SMTPsettings
	{
		public string FromName { get; set; }
		public string From { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public SecureSocketOptions SecureSocketOptions { get; set; }
	}
}
