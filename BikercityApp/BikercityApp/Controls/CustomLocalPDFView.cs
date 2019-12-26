using Xamarin.Forms;

namespace BikercityApp.Controls
{
	public class CustomLocalPDFView : WebView
	{
		public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
			returnType: typeof(string),
			declaringType: typeof(CustomLocalPDFView),
			defaultValue: default(string));

		public string Uri
		{
			get
			{
				return (string)GetValue(UriProperty);
			}
			set
			{
				SetValue(UriProperty, value);
			}
		}

		public UrlWebViewSource GetWebViewSource()
		{
			return (UrlWebViewSource)base.Source;
		}
	}
}
