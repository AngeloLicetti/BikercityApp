using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.User.DireccionServicio
{
        public class AgregarDireccionInputModel
        {
            public string direccion { get; set; }
            public string lote { get; set; }
            public string urbanizacion { get; set; }
            public string referencia { get; set; }
            public bool isActive { get; set; }
            public string distrito { get; set; }
            public int idCliente { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string email { get; set; }
            public string numeroTelefonico { get; set; }
        }
        public class ActualizarDireccionInputModel
        {
            public int idDireccion { get; set; }
            public string direccion { get; set; }
            public string lote { get; set; }
            public string urbanizacion { get; set; }
            public string referencia { get; set; }
            public string distrito { get; set; }
            public int idCliente { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string email { get; set; }
            public string numeroTelefonico { get; set; }
        }
        public class EliminarDireccionInputModel
        {
            public int idDireccion { get; set; }
            public int idCliente { get; set; }
        }

        public class PredeterminarDireccionInputModel
        {
            public int idDireccion { get; set; }
            public int idCliente { get; set; }
        }
}
