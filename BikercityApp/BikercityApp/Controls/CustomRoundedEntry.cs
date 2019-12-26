using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BikercityApp.Controls
{
	public class CustomRoundedEntry : Entry
	{
		public static readonly BindableProperty BorderColorProperty =
			   BindableProperty.Create("BorderColor", typeof(Color), typeof(CustomRoundedEntry), Color.FromHex("#c4d9e1"));
		public static readonly BindableProperty CornerRadiusProperty =
		   BindableProperty.Create("CornerRadius", typeof(int), typeof(CustomRoundedEntry), 8);
		public static readonly BindableProperty BorderWidthProperty =
		   BindableProperty.Create("BorderWidth", typeof(int), typeof(CustomRoundedEntry), 2);
		public CustomRoundedEntry() : base()
		{

		}
		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}
		public int CornerRadius
		{
			get { return (int)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		public int BorderWidth
		{
			get { return (int)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}
	}

}
