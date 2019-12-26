using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikercityApp.Models.Common;
using BikercityApp.Models.Orden;
using BikercityApp.Services.RequestProvider;

namespace BikercityApp.Services.Ordenes
{
    public class OrdenService : IOrdenService
    {
        private IRequestProvider _requestProvider { get; set; }
        public OrdenService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<List<DetalleOrdenModel>> GetDetalleCompraByID(int idOrden)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/DetalleOrden/ListarDetalleOrdenByIdOrden");
            builder.Query = string.Format("idOrden={0}", idOrden);

            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<List<DetalleOrdenModel>>(uri).ConfigureAwait(false);
            return body;
        }

        public async Task<List<OrdenModel>> GetOrdenByCliente(int id)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Orden/ListarOrdenByIdCliente");
            builder.Query = string.Format("idCliente={0}", id);
            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<List<OrdenModel>>(uri).ConfigureAwait(false);

            return body;
        }

		public async Task<BaseResponse> CreateOrden(CreateOrdenInputModel inputModel)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Orden/Create");
			string uri = builder.ToString();

			var body = await _requestProvider.PostAsync<BaseResponse, CreateOrdenInputModel>(uri, inputModel).ConfigureAwait(false);
			return body;
		}

        public async Task<Boolean> VerifyDetalleOrdenByIdCliente(int idCliente, int detalleProd)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/DetalleOrden/ValidateDetelleProd");
            builder.Query = string.Format("idCliente={0}&detalleProd={1}", idCliente, detalleProd);

            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<Boolean>(uri).ConfigureAwait(false);
            return body;
        }
    }
}
