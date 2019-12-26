using BikercityApp.ViewModels.Base;
using System;
using Xamarin.Forms;

namespace BikercityApp.Models.Servicios
{
	public class CitaServicioModel : ExtendedBindableObject
	{
		public int idCita { get; set; }

		public int idSolicitudServicio { get; set; }
		public int idHoras { get; set; }
		public int idEstadoCita { get; set; }
		public TimeSpan horaInicio { get; set; }
		public TimeSpan horaFin { get; set; }
		public string DescripcionHoras { get; set; }

		public string idServicio { get; set; }
		public string EstadoCita { get; set; }
		public string IconEstado
		{
			get
			{
				var iconText = "calendar-check";
				if (EstadoCita.ToUpper() == "POSPUESTO")
					iconText = "calendar-day";
				if (EstadoCita.ToUpper() == "TERMINADO")
					iconText = "calendar-alt";
				if (EstadoCita.ToUpper() == "CANCELADO")
					iconText = "calendar-times";
				return iconText;
			}
		}
		public Color ColorEstado
		{
			get
			{
				var color = Color.FromHex("#28a745");
				if (EstadoCita.ToUpper() == "POSPUESTO")
					color = Color.FromHex("#ffc107");
				if (EstadoCita.ToUpper() == "TERMINADO")
					color = Color.FromHex("#007bff");
				if (EstadoCita.ToUpper() == "CANCELADO")
					color = Color.FromHex("#dc3545");
				return color;
			}
		}

        public bool IsEditable
        {
            get
            {
                var isEditable = true;
                if (EstadoCita.ToUpper() == "POSPUESTO")
                    isEditable = false;
                if (EstadoCita.ToUpper() == "TERMINADO")
                    isEditable = false;
                if (EstadoCita.ToUpper() == "CANCELADO")
                    isEditable = false;
                return isEditable;
            }
        }

        public bool IsCancelable
        {
            get
            {
                var isCancelable = true;
                if (EstadoCita.ToUpper() == "TERMINADO")
                    isCancelable = false;
                if (EstadoCita.ToUpper() == "CANCELADO")
                    isCancelable = false;
                return isCancelable;
            }
        }
        //private string _isSameDate;

        //public string IsSameDate
        //{
        //	get { return _isSameDate; }
        //	set { _isSameDate = value; RaisePropertyChanged(() => IsSameDate); }
        //}


        public DateTime Fecha { get; set; }
		public string FechaFormated { get { return Fecha.ToString("MMM dd"); } }
		public string FechaFullFormated { get { return Fecha.ToString("MMM dd") + ", " + DescripcionHoras; } }
	}
}
