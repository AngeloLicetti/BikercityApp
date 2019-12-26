using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Catalogo.ResenaServicio
{
    public class CrearResenaInputModel
    {
        public string descripcion { get; set; }
        public int calificacion { get; set; }
        public int idDetalleProducto { get; set; }
        public int idCliente { get; set; }

    }
    public class CrearResenaResponse
    {
        public int result { get; set; }
        //public bool result { get; set; }
    }
}
