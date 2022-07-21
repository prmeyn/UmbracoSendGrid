using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.Composing;

namespace UmbracoSendGrid.Setup
{
	public class SendGridComponent : IComponent
	{
		private readonly IConfiguration _config;

		public SendGridComponent(IConfiguration config)
		{
			_config = config;
		}

		public void Initialize()
		{
			var smtpAppSettingsConfigPath = "Umbraco:Global:Smtp";
			EmailSender.Initialize(new SMTPsettings()
			{
				FromName = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:FromName"),
				From = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:From"),
				Host = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:Host"),
				Port = _config.GetValue<int>($"{smtpAppSettingsConfigPath}:Port"),
				Username = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:Username"),
				Password = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:Password"),
				SecureSocketOptions = (SecureSocketOptions)Enum.Parse(typeof(SecureSocketOptions), _config.GetValue<string>($"{smtpAppSettingsConfigPath}:SecureSocketOptions"))
			});
		}

		public void Terminate()
		{
			
		}
	}
}
