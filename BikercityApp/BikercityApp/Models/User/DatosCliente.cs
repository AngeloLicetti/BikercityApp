using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.User
{
    public class DatosCliente
    {
        public int idDatosCliente { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string direccion { get; set; }
        public string distrito { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int idCliente { get; set; }
    }
}
