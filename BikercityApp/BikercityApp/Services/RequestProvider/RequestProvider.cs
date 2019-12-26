using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using BikercityApp.Exceptions;
using BikercityApp.Models.Common;

namespace BikercityApp.Services.RequestProvider
{
	public class RequestProvider : IRequestProvider
	{
		private readonly JsonSerializerSettings _serializerSettings;
		private static HttpClient instance;

		//private IDialogService _dialogservice;

		public RequestProvider(
			//IDialogService dialogservice
			)
		{
			_serializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				NullValueHandling = NullValueHandling.Ignore
			};
			_serializerSettings.Converters.Add(new StringEnumConverter());
			//_dialogservice = dialogservice;
		}

		public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
		{
			try
			{
				HttpClient httpClient = await CreateHttpClient(uri);
				HttpResponseMessage response = await httpClient.GetAsync(uri).ConfigureAwait(false);

				await HandleResponse(response).ConfigureAwait(false);
				string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				TResult result = await Task.Run(() =>
					JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings)).ConfigureAwait(false);

				return result;
			}
			catch (ServiceErrorCodeException ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.CodeMessage);
                return default(TResult);
			}
			catch (Exception exe)
			{
				var properties = new Dictionary<string, string> {
					{ "Type", Models.Common.Constants.ErrorTypes.Service },
					{ "Url", uri },
					{ "Method", Models.Common.Constants.HttpMethod.Get },
				};
                //Crashes.TrackError(exe, properties);
                System.Diagnostics.Debug.WriteLine(exe.Message);
                return default(TResult);
            }
        }

		public async Task<FileDownloadHeaderModel> GetByteAsync(string uri, string token = "")
		{
			try
			{
				HttpClient httpClient = await CreateHttpClient(uri);
				HttpResponseMessage response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

				await HandleResponse(response).ConfigureAwait(false);
				byte[] result = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
				IEnumerable<string> values;
				string filename = Constants.HttpHeaderConstants.FilenameDefault;
				if (response.Headers.TryGetValues(Constants.HttpHeaderConstants.FilenameHeader, out values))
				{
					filename = values.FirstOrDefault();
				}

				FileDownloadHeaderModel output = new FileDownloadHeaderModel
				{
					FileName = filename,
					FileContent = result
				};

				return output;
			}
			catch (ServiceErrorCodeException ex)
			{
				return new FileDownloadHeaderModel();
			}
			catch (Exception exe)
			{
				var properties = new Dictionary<string, string> {
					{ "Type", Models.Common.Constants.ErrorTypes.Service },
					{ "Url", uri },
					{ "Method", Models.Common.Constants.HttpMethod.Get },
				};
				return new FileDownloadHeaderModel();
			}
		}

		public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
		{
			try
			{
				HttpClient httpClient = await CreateHttpClient(uri);

				if (!string.IsNullOrEmpty(header))
				{
					AddHeaderParameter(httpClient, header);
				}

				var content = new StringContent(JsonConvert.SerializeObject(data));
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				HttpResponseMessage response = await httpClient.PostAsync(uri, content);

				await HandleResponse(response);
				string serialized = await response.Content.ReadAsStringAsync();

				TResult result = await Task.Run(() =>
					JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

				return result;
			}
			catch (ServiceErrorCodeException ex)
			{
				throw;
			}
			catch (Exception exe)
			{
				var properties = new Dictionary<string, string> {
					{ "Type", Models.Common.Constants.ErrorTypes.Service },
					{ "Url", uri },
					{ "Method", Models.Common.Constants.HttpMethod.Post },
				};
				//Crashes.TrackError(exe, properties);
				return default(TResult);
			}
		}

		public async Task<TOutput> PostAsync<TOutput, TInput>(string uri, TInput data, string token = "", string header = "")
		{
			try
			{
				HttpClient httpClient = await CreateHttpClient(uri);

				if (!string.IsNullOrEmpty(header))
				{
					AddHeaderParameter(httpClient, header);
				}

	
				var content = new StringContent((JsonConvert.SerializeObject(data)));
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				HttpResponseMessage response = await httpClient.PostAsync(uri, content).ConfigureAwait(false);

				await HandleResponse(response).ConfigureAwait(false);
				string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				TOutput result = await Task.Run(() =>
					JsonConvert.DeserializeObject<TOutput>(serialized, _serializerSettings)).ConfigureAwait(false);

				return result;
			}
			catch (ServiceErrorCodeException ex)
			{
				throw;
			}
			catch (Exception exe)
			{
				var properties = new Dictionary<string, string> {
					{ "Type", Models.Common.Constants.ErrorTypes.Service },
					{ "Url", uri },
					{ "Method", Models.Common.Constants.HttpMethod.Post },
				};
				//Crashes.TrackError(exe, properties);
				return default(TOutput);
			}
		}

		public async Task<TOutput> PutAsync<TOutput, TResult>(string uri, TResult data, string token = "", string header = "")
		{
			try
			{
				HttpClient httpClient = await CreateHttpClient(uri);

				if (!string.IsNullOrEmpty(header))
				{
					AddHeaderParameter(httpClient, header);
				}

				var content = new StringContent(JsonConvert.SerializeObject(data));
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				HttpResponseMessage response = await httpClient.PutAsync(uri, content).ConfigureAwait(false);

				await HandleResponse(response).ConfigureAwait(false);
				string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				TOutput result = await Task.Run(() =>
					JsonConvert.DeserializeObject<TOutput>(serialized, _serializerSettings)).ConfigureAwait(false);

				return result;
			}
			catch (ServiceErrorCodeException ex)
			{
				throw;
			}
			catch (Exception exe)
			{
				var properties = new Dictionary<string, string> {
					{ "Type", Models.Common.Constants.ErrorTypes.Service },
					{ "Url", uri },
					{ "Method", "Put" },
				};
				//Crashes.TrackError(exe, properties);

				throw;
			}
		}

		public async Task<bool> DeleteAsync(string uri, string token = "")
		{
			try
			{
				HttpClient httpClient = await CreateHttpClient(uri);
				await httpClient.DeleteAsync(uri);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private async Task<HttpClient> CreateHttpClient(string uri = "")
		{
			var httpClient = GetInstance();

			//if (GlobalSetting.Instance.AuthToken != null)
			//{
			//    if (GlobalSetting.Instance.LastTokenDate.AddSeconds(GlobalSetting.Instance.AuthToken.ExpiresIn) < DateTime.Now)
			//    {
			//        await _identityService.Refresh();
			//    }
			//    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetting.Instance.AuthToken.AccessToken);
			//}
			//else
			//{
			//    try
			//    {
			//        httpClient.DefaultRequestHeaders.Remove("Authorization");
			//    }
			//    catch (Exception)
			//    {
			//        //Do nothing
			//    }
			//}
			return httpClient;
		}

		private static HttpClient GetInstance()
		{
			if (instance == null)
			{
				instance = new HttpClient();
				instance.Timeout = TimeSpan.FromSeconds(30);
				instance.MaxResponseContentBufferSize = 256000;
				instance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			}

			return instance;
		}

		private void AddHeaderParameter(HttpClient httpClient, string parameter)
		{
			if (httpClient == null)
				return;

			if (string.IsNullOrEmpty(parameter))
				return;

			httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
		}

		private async Task HandleResponse(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				if (Constants.ErrorServiceDictionary.ContainsKey((int)response.StatusCode))
				{
					throw new ServiceErrorCodeException((int)response.StatusCode, Constants.ErrorServiceDictionary[(int)response.StatusCode]);
				}
				var content = await response.Content.ReadAsStringAsync();

				if (response.StatusCode == HttpStatusCode.Forbidden ||
					response.StatusCode == HttpStatusCode.Unauthorized)
				{
					throw new ServiceAuthenticationException(content);
				}

				throw new HttpRequestException(content);
			}
		}
	}
}
