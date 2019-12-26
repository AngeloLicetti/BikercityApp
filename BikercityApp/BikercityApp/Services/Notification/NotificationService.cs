using BikercityApp.Helpers;
using BikercityApp.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Notification
{
    public class NotificationService : INotificationService
    {
		private IRequestProvider _requestProvider { get; set; }
		public NotificationService(IRequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}
		public async Task<bool> registerToken(string fcmToken)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Notification/RegisterToken");

			RegisterTokenInput input = new RegisterTokenInput
			{
				IdCliente = Settings.idCliente,
				FCMToken = fcmToken
			};
			string uri = builder.ToString();
			var body = await _requestProvider.PostAsync<bool, RegisterTokenInput>(uri, input).ConfigureAwait(false);

			return body;
		}
	}
	public class RegisterTokenInput 
	{
		public int IdCliente { get; set; }
		public string FCMToken { get; set; }
	}
}
