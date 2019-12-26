using System;
using System.IO;

using System.Threading.Tasks;

using Android.Content;

using Android.Support.V4.Content;
using BikercityApp.Droid.Dependency;
using BikercityApp.Helpers;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SharePdf))]
namespace BikercityApp.Droid.Dependency
{
	public class SharePdf : ISharePdf
	{
		private readonly Context context;

		public SharePdf()
		{
			context = Android.App.Application.Context;
		}

		public string Save(MemoryStream fileStream, string title)
		{
			throw new NotImplementedException();
		}

		public Task Share(string title, string message, string filePath)
		{
			try
			{
				//var uri = Android.Net.Uri.Parse("file://" + filePath);
				string packageName = Android.App.Application.Context.PackageName;
				Android.Net.Uri uri = FileProvider.GetUriForFile(context, packageName + ".provider", new Java.IO.File(filePath));
				//"com.companyname.BikercityApp.fileprovider"
				var contentType = "application/pdf";
				var intent = new Intent(Intent.ActionSend, uri);
				intent.AddFlags(ActivityFlags.GrantReadUriPermission);
				intent.PutExtra(Intent.ExtraStream, uri);
				intent.PutExtra(Intent.ExtraText, string.Empty);
				intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);
				intent.SetType(contentType);
				var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
				chooserIntent.SetFlags(ActivityFlags.ClearTop);
				chooserIntent.SetFlags(ActivityFlags.NewTask);
				context.StartActivity(chooserIntent);
				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
			}
			return Task.FromResult(true);
		}

		public async Task<string> WritePdfFile(byte[] bytes, string Name)
		{
			try
			{
				var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				directory = Path.Combine(directory, Android.OS.Environment.DirectoryDownloads);
				string filePath = Path.Combine(directory.ToString(), Name);
				File.WriteAllBytes(filePath, bytes);
				return filePath;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}