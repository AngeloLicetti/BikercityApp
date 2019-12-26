using System.Threading.Tasks;

namespace BikercityApp.Helpers
{
	public interface IFileHelper
	{
		string GetPath(string file);
		Task<string> WritePdfFile(byte[] bytes, string Name);
		//Stream ReadPdfFile(string Path);
	}
}
