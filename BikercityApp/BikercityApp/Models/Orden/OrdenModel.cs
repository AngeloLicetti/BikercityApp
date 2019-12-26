using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Orden
{
	public class OrdenModel
	{
		public int idOrden { get; set; }
		public int idDetalleProducto { get; set; }
		public string estado { get; set; }
		public int cantidad { get; set; }
		public double precio { get; set; }
		public DateTime fecha { get; set; }
		public string fechaFormateada { get { return fecha.ToString("dd/MM/yyyy"); } }
		public double descuentotal { get; set; }
		public double preciototal { get; set; }
	}

}
