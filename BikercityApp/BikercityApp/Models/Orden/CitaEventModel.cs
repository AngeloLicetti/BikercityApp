using System;

namespace BikercityApp.Models.Orden
{
	public class CitaEventModel
	{
		public string descripcion { get; set; }
		public TimeSpan horaInicio { get; set; }
		public TimeSpan horaFin { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public string titulo { get; set; }
		public string estadoCita { get; set; }
	}
}
