using System;

namespace BikercityApp.Models.Servicios.ModeloServicio
{
	public class CitaInputModel
	{
		public DateTime Fecha { get; set; }
		public int IdSolicitudServicio { get; set; }
		public int IdHoras { get; set; }
		public int IdEstadoCita { get; set; }
	}
	public class CitaUpdateInputModel
	{
		public DateTime Fecha { get; set; }
		public int IdHoras { get; set; }
		public int IdCita { get; set; }
	}
	public class CitaCancelInputModel
	{
		public int IdCita { get; set; }
	}
}
