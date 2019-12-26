using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Catalogo
{
    public class Orden
    {
        public int idOrden { get; set; }
        public string descripcion { get; set; }
        public string comentario { get; set; }    
        public string boucher { get; set; }
        public DateTime fecha { get; set; }
        public int idCarrito { get; set; }
        public int idEstadoOrden { get; set; }
        public int idDireccion { get; set; }
        public int numIntentos { get; set; }
    }
}
