using System;
using System.Collections.Generic;
using BikercityApp.Models.Catalogo;
using System.Text;
using System.Threading.Tasks;
using BikercityApp.Models.Common;
using BikercityApp.Models.Catalogo.ResenaServicio;

namespace BikercityApp.Services.Catalogo
{
    public interface ICatalogoService
    {
        Task<List<DetalleProductoModel>> GetDetalleProductoLista();
        Task<List<TipoProductoModel>> GetAllTipoProducto();
		Task<DetalleProductoModel> GetDetalleProductoById(int idDetalleProducto);
        Task<List<ComentarioModel>> GetComentarioByID(int idDetalleProducto);
        Task<BaseResponse> CrearResena(CrearResenaInputModel input);
    }
}
