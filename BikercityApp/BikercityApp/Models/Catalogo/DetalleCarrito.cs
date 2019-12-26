using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Catalogo
{
    public class DetalleCarrito
    {
        public int idDetalleCarrito { get; set; }
        public string descripcion { get; set; }
        public int stockWeb { get; set; }
        public int garantia { get; set; }
        public DateTime fechaRegistro { get; set; }
        public float descuento { get; set; }
        public DateTime fechaDescuento { get; set; }
        public int idEstadoProducto { get; set; }
        public int idInventario { get; set; }
        public float precioVenta { get; set; }
    }
}
