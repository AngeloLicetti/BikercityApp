using BikercityApp.Helpers;
using BikercityApp.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using BikercityApp.Models.User;
using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Catalogo.ModeloServicio;
using BikercityApp.Models.Common;
using BikercityApp.Models.User.TarjetaServicio;

namespace BikercityApp.Services.Tarjeta
{
    public class TarjetaService : ITarjetaService
    {
        private IRequestProvider _requestProvider { get; set; }
        public TarjetaService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<bool> agregarTarjeta(TarjetaModel tarjeta)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Tarjeta/Create");
            string uri = builder.ToString();

            var body = await _requestProvider.PostAsync<bool, TarjetaModel>(uri, tarjeta).ConfigureAwait(false);
            return body;
        }
        public async Task<List<TarjetaModel>> getListaTarjeta()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Tarjeta/GetAllByIdCliente");
            builder.Query = string.Format("idCliente={0}", Settings.idCliente);

            string uri = builder.ToString();

            var body = await _requestProvider.GetAsync<List<TarjetaModel>>(uri).ConfigureAwait(false);
            return body;
        }

		public async Task<BaseResponse> predeterminarTarjeta(PredeterminarTarjetaInputModel tarjeta)
        {
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Tarjeta/ActDesDireccionCliente");
			string uri = builder.ToString();
			var body = await _requestProvider.PutAsync<BaseResponse, PredeterminarTarjetaInputModel>(uri, tarjeta).ConfigureAwait(false);
			return body;
		}
		public async Task<TarjetaModel> getTarjetaPredeterminada()
        {
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Tarjeta/ListarActivoByIdCliente");
			builder.Query = string.Format("IdCliente={0}", Settings.idCliente);

			string uri = builder.ToString();

			var body = await _requestProvider.GetAsync<TarjetaModel>(uri).ConfigureAwait(false);
			return body;
		}
	}
}
