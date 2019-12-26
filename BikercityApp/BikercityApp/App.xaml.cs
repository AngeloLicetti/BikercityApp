using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BikercityApp.Views;
using System.Threading.Tasks;
using BikercityApp.ViewModels.Base;
using BikercityApp.Services.Navigation;
using DLToolkit.Forms.Controls;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BikercityApp
{
	public partial class App : Application
	{

		public App()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTU5ODZAMzEzNzJlMzEyZTMwQ3FFYnc2RnZYQVd0WVlHTTZMTUtHTVk3Q2Vnd3JITEF6OGM1cG9aK1RuRT0=");
            InitializeComponent();
            FlowListView.Init();
            InitApp();
		}

		private void InitApp()
		{
			ViewModelLocator.RegisterDependencies(false);
		}

		private Task InitNavigation()
		{
			var navigationService = ViewModelLocator.Resolve<INavigationService>();
			return navigationService.InitializeAsync();
		}

		protected override async void OnStart()
		{
			base.OnStart();
			//Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			//{
			//	AppCenter.Start("android=5b69b55f-4c19-4c44-806f-0ee5184482f8" + "uwp={Your UWP App secret here};" + "ios=f8d787ab-71c4-4b72-9255-f3594a9e3f25;", typeof(Analytics), typeof(Crashes));
			//});
			await InitNavigation();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		protected override async void OnSleep()
		{
			base.OnSleep();
		}
	}
}
