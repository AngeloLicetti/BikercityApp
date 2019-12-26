using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikercityApp.Models.Common;
using BikercityApp.Models.User;
using BikercityApp.Models.User.DireccionServicio;

namespace BikercityApp.Services.Direccion
{
    public interface IDireccionService
    {
        Task<List<DireccionModel>> getDireccionPredeterminada();
        Task<bool> agregarDireccion(DireccionModel direccion);
        Task<List<DireccionModel>> getListaDirecciones();
        Task<BaseResponse> eliminarDireccion(EliminarDireccionInputModel direccion);
        Task<bool> actualizarDireccion(DireccionModel direccion);
		Task<BaseResponse> predeterminarDireccion(PredeterminarDireccionInputModel direccion);
	}

}   
