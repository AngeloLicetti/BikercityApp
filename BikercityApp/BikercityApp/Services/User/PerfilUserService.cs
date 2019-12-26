using BikercityApp.Models.User;
using BikercityApp.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.User
{
    public class PerfilUserService : IPerfilUserService
    {
        private IRequestProvider _requestProvider { get; set; }
        public PerfilUserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<DatosCliente> GetByID(int id)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Cliente/GetById");
            builder.Query = string.Format("Id={0}", id);
            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<DatosCliente>(uri).ConfigureAwait(false);
            return body;

        }
    }
}
