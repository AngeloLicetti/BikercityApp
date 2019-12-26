using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikercityApp.Models.User;
using BikercityApp.Models.User.ClienteServicio;
using BikercityApp.Models.User.ModeloServicio;
using BikercityApp.Services.RequestProvider;

namespace BikercityApp.Services.Cliente
{
    public class ClienteService : IClienteService
    {
        private IRequestProvider _requestProvider { get; set; }
        public ClienteService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<LoginClienteResponse> LoginCliente(LoginClienteInput input)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Cliente/LoginCliente");
            

            string uri = builder.ToString();
            var body = await _requestProvider.PostAsync<LoginClienteResponse,LoginClienteInput>(uri,input).ConfigureAwait(false);


            return body;
        }

        public async Task<bool> agregarCliente(AgregarClienteInputModel clienteInputModel)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Cliente/Create");
            string uri = builder.ToString();


            var body = await _requestProvider.PostAsync<bool, AgregarClienteInputModel>(uri, clienteInputModel).ConfigureAwait(false);
            return body;
        }
    }
}
