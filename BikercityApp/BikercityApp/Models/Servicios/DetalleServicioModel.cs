using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Servicios
{
    public class DetalleServicioModel
    {
        public int idSolicitudServicio { get; set; }
        public int idCliente { get; set; }
        public string nombreContacto { get; set; }
        public string telefono { get; set; }
        public string correoContacto { get; set; }
        public string direccionContacto { get; set; }
        public string comentario { get; set; }
        public int idestadoServicio { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string fechaFormateada { get { return fechaRegistro.ToString("dd/MM/yyyy"); } }
        public string urlpdf { get; set; }
        public string nombrePdf { get { return String.Format("Cotizacion_{0}.pdf", idSolicitudServicio); }  }
        public string comentarioAdmin { get; set; }
        public double cotizacion { get; set; }
        public string reporteservicio { get; set; }
        public string descripcionEstado { get; set; }
        public string tituloServicio { get; set; }
        public string descripcionServicio { get; set; }
        public string foto { get; set; }
        public int iddetalleServicio { get; set; }
        public string tituloDetalle { get; set; }
        public string descripcionDetalle { get; set; }
        public List <CitaServicioModel> citas { get; set; }
        public int idServicio { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public bool isActive { get; set; }
        public List<ListDetalleServicio> listDetalleServicio { get; set; }
    }
    public class ListDetalleServicio
    {
        public int idDetalleServicio { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string idServicio { get; set; }
    }


}
