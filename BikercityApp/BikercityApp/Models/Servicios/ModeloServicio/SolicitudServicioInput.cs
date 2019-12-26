using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Servicios.ModeloServicio
{
    public class SolicitudServicioInput
    {
        public int idServicio { get; set; }
        public int idEstadoServicio { get; set; }
        public string nombreContacto { get; set; }
        public string correoContacto { get; set; }
        public string telefono { get; set; }
        public int idCliente { get; set; }
        public string comentario { get; set; }
        public string direccionContacto { get; set; }

    }
}
