using System.IO;
using System.Threading.Tasks;

namespace BikercityApp.Helpers
{
	public interface ISharePdf
	{
		Task Share(string title, string message, string filePath);
		string Save(MemoryStream fileStream, string title);
		Task<string> WritePdfFile(byte[] bytes, string Name);
	}
}
