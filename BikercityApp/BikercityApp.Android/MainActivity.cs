using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Plugin.Permissions;
using Firebase;
using System.Threading.Tasks;
using Firebase.Messaging;

namespace BikercityApp.Droid
{
    [Activity(Label = "Bikercity", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());
			FirebaseApp.InitializeApp(this.BaseContext);
			//Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
			//{
			//	await Task.Delay(5000);
			//	//FirebaseMessaging.Instance.SubscribeToTopic("all");
			//});

		}

		public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
		{
			//ImageService.Instance.InvalidateMemoryCache();
			base.OnTrimMemory(level);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			try
			{
				base.OnActivityResult(requestCode, resultCode, data);
			}
			catch (Exception)
			{
				//Do nothing
			}
		}
	}
}


