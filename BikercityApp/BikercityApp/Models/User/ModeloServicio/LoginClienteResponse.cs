using BikercityApp.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.User.ModeloServicio
{
    public class LoginClienteResponse : BaseResponse
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
    }
}
