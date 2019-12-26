using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikercityApp.Views.Templates
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InformationModalTemplate : ContentView
	{
		public InformationModalTemplate()
		{
			InitializeComponent();
		}

		public static readonly BindableProperty HeaderTemplateProperty =
				   BindableProperty.Create<ContentView, View>(p => p.Content, null,
				   BindingMode.TwoWay, null,
				   new BindableProperty.BindingPropertyChangedDelegate<View>(HeaderTemplateChanged), null, null);

		public View HeaderTemplate
		{
			get { return (View)GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}

		static void HeaderTemplateChanged(BindableObject obj, View oldValue, View newValue)
		{
			var control = (InformationModalTemplate)obj;
			control.LayerContent.Children.Add(newValue);

		}

		public static readonly BindableProperty FFBackgroundColorProperty = BindableProperty.Create(
													   propertyName: "FFBackgroundColor",
													   returnType: typeof(Color),
													   declaringType: typeof(InformationModalTemplate),
													   defaultValue: Color.White,
													   defaultBindingMode: BindingMode.TwoWay,
													   propertyChanged: FFBackgroundColorPropertyChanged);
		private static void FFBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (InformationModalTemplate)bindable;
			Color colorNuevo = (Color)newValue;
			control.FrameModal.BackgroundColor = colorNuevo;
		}
		public Color FFBackgroundColor
		{
			get
			{
				return (Color)GetValue(FFBackgroundColorProperty);
			}
			set
			{
				base.SetValue(FFBackgroundColorProperty, value);
			}
		}

		public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
													   propertyName: "TitleColor",
													   returnType: typeof(Color),
													   declaringType: typeof(InformationModalTemplate),
													   defaultValue: Color.FromHex("#777777"),
													   defaultBindingMode: BindingMode.TwoWay,
													   propertyChanged: TitleColorPropertyChanged);
		private static void TitleColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (InformationModalTemplate)bindable;
			Color colorNuevo = (Color)newValue;
			control.Title.TextColor = colorNuevo;
		}
		public Color TitleColor
		{
			get
			{
				return (Color)GetValue(TitleColorProperty);
			}
			set
			{
				base.SetValue(TitleColorProperty, value);
			}
		}

		public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(
													   propertyName: "ButtonColor",
													   returnType: typeof(Color),
													   declaringType: typeof(InformationModalTemplate),
													   defaultValue: Color.FromHex("#009fda"),
													   defaultBindingMode: BindingMode.TwoWay,
													   propertyChanged: ButtonColorPropertyChanged);
		private static void ButtonColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (InformationModalTemplate)bindable;
			Color colorNuevo = (Color)newValue;
			control.ButtonClose.TextColor = colorNuevo;
		}
		public Color ButtonColor
		{
			get
			{
				return (Color)GetValue(ButtonColorProperty);
			}
			set
			{
				base.SetValue(ButtonColorProperty, value);
			}
		}
	}
}