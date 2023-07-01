# Setup procedure

The `appsettings.json` file should SMTP settings for your SendGrid account
```json
{
	"Umbraco": {
		"CMS":{
			"Global": {
				"Smtp": {
					"FromName": "Peter Brown",
					"From": "someFrom@email.com",
					"Host": "smtp.sendgrid.net",
					"Port": 465,
					"Username": "apikey",
					"Password": "SG.xxxxYourPasswordxxxxx",
					"SecureSocketOptions": "SslOnConnect"
				}
			}
		},
	}
}
```

```csharp
using UmbracoSendGrid;
```

Sample code to use the regular SMTP Client with SendGrid
```csharp
EmailSender.SendEmail(
	toEmailsAndName: new Dictionary<string, string>()
	{
	{
		"someTo@email.com" , "Some Person"
	}
	},
	subject: "Some subject",
	textBody: "Hello World",
	htmlBody: "<h1>Hello World</h1>");
```

Sample code to use SendGrid's native API using SendGrid dynamic templates
```csharp
var valuesToBeSubstitutedInTheEmail = new Dictionary<string, string>() {
				{ "SomeKey", "SomeValue" },
				{ "SomeOtherKey", "SomeOtherValue" }
			};
```

```csharp
EmailSender.SendEmail(
	toEmailsAndName: new Dictionary<string, string>()
	{
		{
			"someTo@email.com" , "Some Person"
		}
	},
	templateIdAndTemplate: new KeyValuePair<string, Dictionary<string, string>>(
		"aValidSendGridDynamicTemplateID", valuesToBeSubstitutedInTheEmail)
);
```
