using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.Composing;

namespace UmbracoSendGrid.Setup
{
	public class SendGridComponent : IComponent
	{
		private readonly IHostEnvironment _env;
		private readonly IConfiguration _config;

		public SendGridComponent(IHostEnvironment env, IConfiguration config)
		{
			_env = env;
			_config = config;
		}

		public void Initialize()
		{
			var smtpAppSettingsConfigPath = "Umbraco:CMS:Global:Smtp";
			EmailSender.Initialize(_env?.IsEnvironment("Local") ?? true, new SMTPsettings()
			{
				FromName = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:FromName"),
				From = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:From"),
				Host = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:Host"),
				Port = _config.GetValue<int>($"{smtpAppSettingsConfigPath}:Port"),
				Username = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:Username"),
				Password = _config.GetValue<string>($"{smtpAppSettingsConfigPath}:Password"),
				SecureSocketOptions = (Umbraco.Cms.Core.Configuration.Models.SecureSocketOptions)Enum.Parse<SecureSocketOptions>(_config.GetValue<string>($"{smtpAppSettingsConfigPath}:SecureSocketOptions"))
			});
		}

		public void Terminate()
		{
			
		}
	}
}
