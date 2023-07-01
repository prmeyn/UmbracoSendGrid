using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoSendGrid.Setup
{
	public sealed class SendGridComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.AddComponent<SendGridComponent>();
		}
	}
}
