using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.User.TarjetaServicio

{
        public class AgregarTarjetaInputModel
        {
            public string numeroTarjeta { get; set; }
            public string nombre { get; set; }
            public string apellidos { get; set; }
            public string direccion { get; set; }
            public int mes { get; set; }
            public int año { get; set; }
            public int cvv { get; set; }
            public int idCliente { get; set; }
        }
        public class ActualizarTarjetaInputModel
		{
            public string numeroTarjeta { get; set; }
            public string nombre { get; set; }
            public string apellidos { get; set; }
            public string direccion { get; set; }
            public int mes { get; set; }
            public int año { get; set; }
            public int cvv { get; set; }
            public int idCliente { get; set; }
        }
        public class EliminarTarjetaInputModel
		{
            public int numeroTarjeta { get; set; }
            public int idCliente { get; set; }
        }

        public class PredeterminarTarjetaInputModel
		{
            public int idTarjeta { get; set; }
            public int idCliente { get; set; }
        }
}