using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Models.Common
{
    public class FileDownloadHeaderModel
	{
		public string FileName { get; set; }
		public byte[] FileContent { get; set; }
	}
}
