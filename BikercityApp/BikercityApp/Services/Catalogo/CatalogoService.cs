using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Catalogo.ModeloServicio;
using BikercityApp.Models.Catalogo.ResenaServicio;
using BikercityApp.Models.Common;
using BikercityApp.Services.RequestProvider;

namespace BikercityApp.Services.Catalogo
{
    class CatalogoService : ICatalogoService
    {
        private IRequestProvider _requestProvider { get; set; }
        public CatalogoService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<List<DetalleProductoModel>> GetDetalleProductoLista()
        {
			try
			{

				UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
				builder.Path = string.Format("api/Producto/GetAllProductoMobile");

				string uri = builder.ToString();
				var body = await _requestProvider.GetAsync<List<DetalleProductoModel>>(uri).ConfigureAwait(false);

				return body;
			}
			catch (Exception ex)
			{
				return new List<DetalleProductoModel>();
			}
        }

        public async Task<List<TipoProductoModel>> GetAllTipoProducto()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/TipoProducto/GetAll");

            string uri = builder.ToString();

            var body = await _requestProvider.GetAsync<List<TipoProductoModel>>(uri).ConfigureAwait(false);


            return body;
        }

        public async Task<DetalleProductoModel> GetDetalleProductoById(int idDetalleProducto)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Producto/GetById");
			builder.Query = string.Format("Id={0}",idDetalleProducto);

            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<DetalleProductoModel>(uri).ConfigureAwait(false);
            
            return body;
        }

        public async Task<List<ComentarioModel>> GetComentarioByID(int idDetalleProducto)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Comentario/ListarComentario");
            builder.Query = string.Format("IdDetalleProducto={0}", idDetalleProducto);

            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<List<ComentarioModel>>(uri).ConfigureAwait(false);

            return body;
        }

        public async Task<BaseResponse> CrearResena(CrearResenaInputModel input)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Comentario/CreateMovil");


            string uri = builder.ToString();
            var body = await _requestProvider.PostAsync<BaseResponse, CrearResenaInputModel>(uri, input).ConfigureAwait(false);


            return body;
        }
    }
}
