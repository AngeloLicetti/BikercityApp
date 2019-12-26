using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Common;
using BikercityApp.Models.Orden;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Ordenes
{
    public interface IOrdenService
    {
        Task<List<OrdenModel>> GetOrdenByCliente(int id);
        Task<List<DetalleOrdenModel>> GetDetalleCompraByID(int idOrden);
		Task<BaseResponse> CreateOrden(CreateOrdenInputModel inputModel);
        Task<Boolean> VerifyDetalleOrdenByIdCliente(int idCliente, int detalleProd);
	}
}
