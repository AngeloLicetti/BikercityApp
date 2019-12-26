using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Orden
{
    public class CreateOrdenInputModel
    {
		public DireccionCreateInputModel direccion { get; set; }
		public TarjetaCreateInputModel tarjeta { get; set; }
		public bool isSaveTarjeta { get; set; }
		public int idCliente { get; set; }
		public double montototal { get; set; }
		
	}
	public class DireccionCreateInputModel
	{
		public string direccion { get; set; }
		public string lote { get; set; }
		public string urbanizacion { get; set; }
		public string referencia { get; set; }
		public bool isActive { get; set; }
		public string distrito { get; set; }
		public int idCliente { get; set; }
		public string nombres { get; set; }
		public string apellidos { get; set; }
		public string email { get; set; }
		public string numeroTelefonico { get; set; }
	}

	public class TarjetaCreateInputModel
	{
		public string numeroTajeta { get; set; }
		public string nombre { get; set; }
		public string apellidos { get; set; }
		public string direccion { get; set; }
		public int mes { get; set; }
		public int año { get; set; }
		public int cvv { get; set; }
		public int idCliente { get; set; }
	}
}
