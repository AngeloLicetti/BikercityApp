using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikercityApp.Views.Servicios
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServicioView : ContentPage
	{

        public ServicioView ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}