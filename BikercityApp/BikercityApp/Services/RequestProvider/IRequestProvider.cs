using BikercityApp.Models.Common;
using System.Threading.Tasks;

namespace BikercityApp.Services.RequestProvider
{
	public interface IRequestProvider
	{
		Task<TResult> GetAsync<TResult>(string uri, string token = "");


		Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");

		Task<TOutput> PostAsync<TOutput, TInput>(string uri, TInput data, string token = "", string header = "");

		Task<TOutput> PutAsync<TOutput, TResult>(string uri, TResult data, string token = "", string header = "");
		Task<FileDownloadHeaderModel> GetByteAsync(string uri, string token = "");
		Task<bool> DeleteAsync(string uri, string token = "");
	}
}
