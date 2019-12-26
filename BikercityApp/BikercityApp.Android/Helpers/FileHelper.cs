using BikercityApp.Droid.Helpers;
using BikercityApp.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace BikercityApp.Droid.Helpers
{
	public class FileHelper : IFileHelper
	{

		public string GetPath(string file)
		{
			return Path.Combine(Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).AbsolutePath, file);
		}

		public async Task<string> WritePdfFile(byte[] bytes, string Name)
		{
			try
			{
				var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				directory = Path.Combine(directory, Android.OS.Environment.DirectoryDocuments);

				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);

				string filePath = Path.Combine(directory.ToString(), Name ?? "ejemplo.pdf");
				File.WriteAllBytes(filePath, bytes);
				return filePath;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}

}