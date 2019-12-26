using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Common
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public string message { get; set; }
        public bool result { get; set; }
    }
}
