using BikercityApp.Models.User;
using BikercityApp.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.User
{
	public class UserService : IUserService
	{
		private IRequestProvider _requestProvider { get; set; }
		public UserService(IRequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		public async Task<List<UserModel>> GetUsuariosLista()
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/User/GetAll");

			string uri = builder.ToString();

			var body = await _requestProvider.GetAsync<List<UserModel>>(uri).ConfigureAwait(false);
			return body;
		}
	}
}
