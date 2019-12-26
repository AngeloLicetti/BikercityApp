using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Orden
{
    public class DetalleOrdenModel
    {
		public int idDetalleOrden { get; set; }
		public int idOrden { get; set; }
		public int idEstadoOrden { get; set; }
		public string descripcionEO { get; set; }
		public int idDetalleProducto { get; set; }
		public string descripcionDO { get; set; }
		public int cantidad { get; set; }
		public string comentario { get; set; }
		public DateTime fechaRegistro { get; set; }
		public int cantidadRechazada { get; set; }
		public string titulo { get; set; }
		public double precio { get; set; }
		public double garantia { get; set; }
		public string foto { get; set; }
		public double descuento { get; set; }
		public int idTipoProducto { get; set; }
		public string caracteristicas { get; set; }
		public string sku { get; set; }
	}
}
