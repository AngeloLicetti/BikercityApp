using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Servicios
{
    public class ServicioModel
    {
        public int idSolicitudServicio { get; set; }
        public int idServicio { get; set; }
        public DateTime fechaRegistro { get; set; } 
        public string fechaFormateada { get { return fechaRegistro.ToString("dd/MM/yyyy"); } }
        public int idEstadoServicio { get; set; }
        public string descripcion { get; set; }
        public string titulo { get; set; }
        public string foto { get; set; }
        public bool isActive { get; set; }
        public List<DetalleServicioModel> listDetalleServicio { get; set; }
        public double cotizaciontotal { get; set; }
    }
}
