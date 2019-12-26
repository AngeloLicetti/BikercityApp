using BikercityApp.Helpers;
using BikercityApp.Services.RequestProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using BikercityApp.Models.User;
using BikercityApp.Models.User.DireccionServicio;
using BikercityApp.Models.Common;

namespace BikercityApp.Services.Direccion
{
    public class DireccionService : IDireccionService
    {
        private IRequestProvider _requestProvider { get; set; }
        public DireccionService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<bool> agregarDireccion(DireccionModel direccion)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Direccion/CreateDireccionCliente");
            string uri = builder.ToString();

            AgregarDireccionInputModel inputModel = new AgregarDireccionInputModel()
            {
                direccion = direccion.direccion,
                lote = direccion.lote,
                urbanizacion = direccion.urbanizacion,
                referencia = direccion.referencia,
                isActive = direccion.IsActive,
                distrito = direccion.distrito,
                idCliente = direccion.idCliente,
                nombres = direccion.nombres,
                apellidos = direccion.apellidos,
                email = direccion.email,
                numeroTelefonico = direccion.numeroTelefonico
            };

            var body = await _requestProvider.PostAsync<bool, AgregarDireccionInputModel>(uri, inputModel).ConfigureAwait(false);
            return body;
        }

        public async Task<bool> actualizarDireccion(DireccionModel direccion)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Direccion/UpdateDireccionCliente");
            string uri = builder.ToString();

            ActualizarDireccionInputModel inputModel = new ActualizarDireccionInputModel()
            {
                idDireccion = direccion.idDireccion,
                direccion = direccion.direccion,
                lote = direccion.lote,
                urbanizacion = direccion.urbanizacion,
                referencia = direccion.referencia,
                distrito = direccion.distrito,
                idCliente = direccion.idCliente,
                nombres = direccion.nombres,
                apellidos = direccion.apellidos,
                email = direccion.email,
                numeroTelefonico = direccion.numeroTelefonico
            };

            var body = await _requestProvider.PostAsync<bool, ActualizarDireccionInputModel>(uri, inputModel).ConfigureAwait(false);
            return body;
        }

        public async Task<BaseResponse> predeterminarDireccion(PredeterminarDireccionInputModel direccion)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Direccion/ActDesDireccionCliente");
            string uri = builder.ToString();
            var body = await _requestProvider.PutAsync<BaseResponse, PredeterminarDireccionInputModel>(uri, direccion).ConfigureAwait(false);
            return body;
        }

        public async Task<BaseResponse> eliminarDireccion(EliminarDireccionInputModel inputModel)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Direccion/DeleteDireccionCliente");
            string uri = builder.ToString();

            var body = await _requestProvider.PostAsync<BaseResponse, EliminarDireccionInputModel>(uri, inputModel).ConfigureAwait(false);
            return body;
        }

        public async Task<List<DireccionModel>> getDireccionPredeterminada()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Direccion/ListarActivoByIdCliente");
            builder.Query = string.Format("IdCliente={0}", Settings.idCliente);

            string uri = builder.ToString();

            var body = await _requestProvider.GetAsync<List<DireccionModel>>(uri).ConfigureAwait(false);
            return body;
        }

        public async Task<List<DireccionModel>> getListaDirecciones()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
            builder.Path = string.Format("api/Direccion/ListarByIdCliente");
            builder.Query = string.Format("idCliente={0}", Settings.idCliente);

            string uri = builder.ToString();

            var body = await _requestProvider.GetAsync<List<DireccionModel>>(uri).ConfigureAwait(false);
            return body;
        }
    }
}
