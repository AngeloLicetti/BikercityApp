using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Catalogo
{
    public class ComentarioModel
    {
        public int idComentario { get; set; }
        public string descripcion { get; set; }
        public int calificacion { get; set; }
        public int idDetalleProducto { get; set; }
        public int idCliente { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string fechaRegistroFormat { get { return fechaRegistro.ToString("dd MMM yyyy"); } }
        public string nombreC { get; set; }
	}
}
