﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikercityApp.Views.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilView : ContentPage
	{
		public PerfilView ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}