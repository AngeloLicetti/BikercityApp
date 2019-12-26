using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Servicios
{
	public class MensajeSolicitudServicio
	{
		public int IdMensaje { get; set; }
		public string Mensaje { get; set; }
		public byte[] Media { get; set; }
		public int? IdUsuario { get; set; }
		public int? IdCliente { get; set; }
		public int IdSolcitudServicio { get; set; }
		public DateTime FechaRegistro { get; set; }
		public string FechaFormated { get { return FechaRegistro.ToString("dd MMMM  HH:mm"); } }
		public bool IsUsuario { get { return IdUsuario.HasValue ? true : false; } } 
		public bool IsCliente { get { return IdCliente.HasValue ? true : false; } }
	}
}
