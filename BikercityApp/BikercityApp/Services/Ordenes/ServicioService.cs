using BikercityApp.Helpers;
using BikercityApp.Models.Common;
using BikercityApp.Models.Orden;
using BikercityApp.Models.Servicios;
using BikercityApp.Models.Servicios.ModeloServicio;
using BikercityApp.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Ordenes
{
    public class ServicioService : IServicioService
    {

        private IRequestProvider _requestProvider { get; set; }
        public ServicioService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<List<ServicioModel>> GetServiceByCliente(int id)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/SolicitudServicio/ListarServicioXUsuario");
            builder.Query = string.Format("idCliente={0}",id);
            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<List<ServicioModel>>(uri).ConfigureAwait(false);

            return body;

        }
		public async Task<List<HorarioModel>> GetHorariosDisponibles(DateTime dateSelected)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Cita/GetHorasDisponibles");
			builder.Query = string.Format("fecha={0}", dateSelected.ToString("MM/dd/yyyy"));
			string uri = builder.ToString();

			var body = await _requestProvider.GetAsync<List<HorarioModel>>(uri).ConfigureAwait(false);
			return body;

		}
		public async Task<BaseResponse> CreateCita(CitaInputModel inputModel)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Cita/CreateCita");
			string uri = builder.ToString();

			var body = await _requestProvider.PostAsync<BaseResponse, CitaInputModel>(uri, inputModel).ConfigureAwait(false);
			return body;
		}
		public async Task<BaseResponse> PosponerCita(CitaUpdateInputModel inputModel)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Cita/PosponerCita");
			string uri = builder.ToString();

			var body = await _requestProvider.PutAsync<BaseResponse, CitaUpdateInputModel>(uri, inputModel).ConfigureAwait(false);
			return body;
		}
		public async Task<BaseResponse> CancelarCita(CitaCancelInputModel inputModel)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Cita/CancelarCita");
			string uri = builder.ToString();

			var body = await _requestProvider.PutAsync<BaseResponse, CitaCancelInputModel>(uri, inputModel).ConfigureAwait(false);
			return body;
		}
		public async Task<List<CitaEventModel>> GetCitaByUser()
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/Cita/GetAllByUserId");
			builder.Query = string.Format("userId={0}", Settings.idCliente);
			string uri = builder.ToString();

			var body = await _requestProvider.GetAsync<List<CitaEventModel>>(uri).ConfigureAwait(false);
			return body;

		}

		public async Task<DetalleServicioModel> GetDetalleServicioByID(int idOrden)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/SolicitudServicio/DetalleOrden");
            builder.Query = string.Format("idSolicitudServicio={0}", idOrden);

            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<DetalleServicioModel>(uri).ConfigureAwait(false);
            return body;

        }
		public async Task<byte[]> GetBytesPdf(string url)
		{
			UriBuilder builder = new UriBuilder(url);
			string uri = builder.ToString();

			var body = await _requestProvider.GetByteAsync(uri).ConfigureAwait(false);
			return body.FileContent;

		}

		public async Task<List<ServicioModel>> GetAllServicios()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Servicio/GetAll");
            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<List<ServicioModel>>(uri).ConfigureAwait(false);

            return body;

        }
        public async Task<DetalleServicioModel> GetServicioByIdServicio(int id)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Servicio/GetDetalleServicio");
            builder.Query = string.Format("IdServicio={0}", id);
            string uri = builder.ToString();
            var body = await _requestProvider.GetAsync<DetalleServicioModel>(uri).ConfigureAwait(false);

            return body;

        }
        public async Task<BaseResponse> SolicitarServicio(SolicitudServicioInput input)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/SolicitudServicio/Create");


            string uri = builder.ToString();
            var body = await _requestProvider.PostAsync<BaseResponse, SolicitudServicioInput>(uri, input).ConfigureAwait(false);


            return body;
        }
		public async Task<BaseResponse> AddMensajeSolicitudServicio(MensajeSolicitudServicio input)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/SolicitudServicio/CreateMensajeSolicitudServicio");

			string uri = builder.ToString();
			var body = await _requestProvider.PostAsync<BaseResponse, MensajeSolicitudServicio>(uri, input).ConfigureAwait(false);
			return body;
		}
		public async Task<List<MensajeSolicitudServicio>> GetMensajeSolicitudServicio(int idSolicitudServicio)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/SolicitudServicio/GetMensajeSolicitudServicio");
			builder.Query = string.Format("idSolicitudServicio={0}", idSolicitudServicio);

			string uri = builder.ToString();
			var body = await _requestProvider.GetAsync<List<MensajeSolicitudServicio>>(uri).ConfigureAwait(false);
			return body;
		}
		public async Task<List<MensajeSolicitudServicio>> GetMensajeCliente(int idCliente)
		{
			UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
			builder.Path = string.Format("api/SolicitudServicio/GetMensajeCliente");
			builder.Query = string.Format("idCliente={0}", idCliente);

			string uri = builder.ToString();
			var body = await _requestProvider.GetAsync<List<MensajeSolicitudServicio>>(uri).ConfigureAwait(false);
			return body;
		}
	}
}
