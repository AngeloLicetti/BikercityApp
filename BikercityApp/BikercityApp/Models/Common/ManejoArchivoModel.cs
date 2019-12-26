using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Common
{
    public class ManejoArchivoModel
    {
		public byte[] PdfBytes { get; set; }
		public string PdfPathFile { get; set; }
		public string Nombre { get; set; }
		public int Id { get; set; }
		public int FlagOrigen { get; set; }
		public string TipoOrigen { get; set; }
	}
}
