using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Servicios
{
    public class HorarioModel
	{
		public int IdHorario { get; set; }
		public string Descripcion { get; set; }
		public TimeSpan HoraFin { get; set; }
		public TimeSpan HoraInicio { get; set; }
    }
}
