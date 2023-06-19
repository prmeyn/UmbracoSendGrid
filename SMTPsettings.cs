using Umbraco.Cms.Core.Configuration.Models;

namespace UmbracoSendGrid
{
	public sealed class SMTPsettings : SmtpSettings
	{
		public string FromName { get; set; }
		public new MailKit.Security.SecureSocketOptions SecureSocketOptions { get; set; }
		/*
		 * Umbraco.Cms.Core.Configuration.Models.SecureSocketOptions matches MailKit.Security.SecureSocketOptions
		 * and defined in Umbraco to avoid having a dependency to Mailkit in Umbraco.Core.
		 */
	}
}
