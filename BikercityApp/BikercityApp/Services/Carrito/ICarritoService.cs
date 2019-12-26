using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Carrito
{
    public interface ICarritoService
    {
		// Task<bool> AgregarDetalleCarrito();
		Task<List<DetalleProductoModel>> getListaCarrito();
		Task<bool> agregarCarrito(DetalleProductoModel detalleProducto);
		Task<bool> eliminarCarrito(DetalleProductoModel detalleProducto);
		Task<BaseResponse> actualizarCantidadCarrito(DetalleProductoModel detalleProducto,int cantidad);
	}
}
