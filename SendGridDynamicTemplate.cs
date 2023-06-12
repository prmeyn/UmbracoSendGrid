using Newtonsoft.Json;

namespace UmbracoSendGrid
{
	public sealed class SendGridDynamicTemplate
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("generation")]
		public string Generation { get; set; }

		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }

		[JsonProperty("versions")]
		public SendGridDynamicTemplateVersion[] Versions { get; set; }
	}

	public sealed class SendGridDynamicTemplateVersion
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("user_id")]
		public int UserId { get; set; }

		[JsonProperty("template_id")]
		public string TemplateId { get; set; }

		[JsonProperty("active")]
		public int Active { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("html_content")]
		public string HtmlContent { get; set; }

		[JsonProperty("plain_content")]
		public string PlainContent { get; set; }

		[JsonProperty("generate_plain_content")]
		public bool GeneratePlainContent { get; set; }

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }

		[JsonProperty("editor")]
		public string Editor { get; set; }

		[JsonProperty("thumbnail_url")]
		public string ThumbnailUrl { get; set; }
	}

}
