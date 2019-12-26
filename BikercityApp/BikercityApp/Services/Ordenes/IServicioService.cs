using BikercityApp.Models.Common;
using BikercityApp.Models.Orden;
using BikercityApp.Models.Servicios;
using BikercityApp.Models.Servicios.ModeloServicio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Ordenes
{
    public interface IServicioService
    {
        Task<List<ServicioModel>> GetServiceByCliente(int id);
        Task<List<ServicioModel>> GetAllServicios();
        Task<DetalleServicioModel> GetServicioByIdServicio(int id);
        Task<DetalleServicioModel> GetDetalleServicioByID(int idOrden);
        Task<BaseResponse> SolicitarServicio(SolicitudServicioInput input);
		Task<List<HorarioModel>> GetHorariosDisponibles(DateTime dateSelected);
		Task<BaseResponse> CreateCita(CitaInputModel inputModel);
		Task<List<CitaEventModel>> GetCitaByUser();
		Task<BaseResponse> PosponerCita(CitaUpdateInputModel inputModel);
		Task<BaseResponse> CancelarCita(CitaCancelInputModel inputModel);
		Task<byte[]> GetBytesPdf(string url);
		Task<BaseResponse> AddMensajeSolicitudServicio(MensajeSolicitudServicio input);
		Task<List<MensajeSolicitudServicio>> GetMensajeSolicitudServicio(int idSolicitudServicio);
		Task<List<MensajeSolicitudServicio>> GetMensajeCliente(int idCliente);
	}
}
