using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.User.ClienteServicio
{
    public class AgregarClienteInputModel
    {
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string direccion { get; set; }
        public string distrito { get; set; }
        public string telef { get; set; }
    }
}
