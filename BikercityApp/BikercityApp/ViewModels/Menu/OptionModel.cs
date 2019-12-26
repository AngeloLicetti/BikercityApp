using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.ViewModels.Menu
{
	public class OptionModel
	{
		public int Id { get; set; }
		public string OptionName { get; set; }
		public bool Enabled { get; set; }
		public string Title { get; set; }
		public int IdPage { get; set; }
		public string Icon { get; set; }
		public string Status { get; set; }
		public Type Target { get; set; }
	}
}
