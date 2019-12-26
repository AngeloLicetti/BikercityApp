using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikercityApp.Views.Catalogo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogoView : ContentPage
    {
        public CatalogoView()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}