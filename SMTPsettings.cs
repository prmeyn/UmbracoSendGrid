using Umbraco.Cms.Core.Configuration.Models;

namespace UmbracoSendGrid
{
	public class SMTPsettings : SmtpSettings
	{
		public string FromName { get; set; }
	}
}
