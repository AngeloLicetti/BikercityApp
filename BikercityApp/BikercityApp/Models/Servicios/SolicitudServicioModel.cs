using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Servicios
{
    public class SolicitudServicioModel
    {
        public int idSolicitudServicio { get; set; }
        public int idCliente { get; set; }
        public string nombreContacto { get; set; }
        public string telefono { get; set; }
        public string correoContacto { get; set; }
        public string direccionContacto { get; set; }
        public string comentario { get; set; }
        public int idServicio { get; set; }
        public int idEstadoServicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string fechaFormateada { get { return FechaRegistro.ToString("dd/MM/yyyy"); } }
        public string UrlPdf { get; set; }
        public string comentarioAdmin { get; set; }
        public string cotizacion { get; set; }
        public string reporteServicio { get; set; }
        public string estadoServicio { get; set; }
    }
}
