
using Android.Content;
using Android.Graphics.Drawables;


using BikercityApp.Controls;
using BikercityApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomRoundedEntry), typeof(CustomRoundedEntryRenderer))]
namespace BikercityApp.Droid.Renderers
{
	public class CustomRoundedEntryRenderer : EntryRenderer

	{
		public CustomRoundedEntryRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				//Control.SetBackgroundResource(Resource.Layout.rounded_shape);
				var gradientDrawable = new GradientDrawable();
				gradientDrawable.SetGradientRadius(((CustomRoundedEntry)Element).CornerRadius);
				gradientDrawable.SetCornerRadius(((CustomRoundedEntry)Element).CornerRadius);
				gradientDrawable.SetStroke(((CustomRoundedEntry)Element).BorderWidth, ((CustomRoundedEntry)Element).BorderColor.ToAndroid());
				gradientDrawable.SetColor(((CustomRoundedEntry)Element).BackgroundColor.ToAndroid());
				Control.SetBackground(gradientDrawable);


				Control.SetPadding(10, Control.PaddingTop, 10,
					Control.PaddingBottom);
			}
		}
	}
}