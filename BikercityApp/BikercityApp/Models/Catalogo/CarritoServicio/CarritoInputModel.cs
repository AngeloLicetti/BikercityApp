using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Catalogo.CarritoServicio
{
    public class AgregarCarritoInputModel
	{
		public int cantidad { get; set; }
		public int idDetalleProducto { get; set; }
		public int idCliente { get; set; }
    }
	public class EliminarCarritoInputModel
	{
		public int idDetalleProducto { get; set; }
		public int idCliente { get; set; }
	}

	public class ActualizarCarrito
	{
		public List<AgregarCarritoInputModel> listCarrito { get; set; }
	}
}
