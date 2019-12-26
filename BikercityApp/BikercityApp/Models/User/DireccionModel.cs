using BikercityApp.ViewModels.Base;
using Xamarin.Forms;

namespace BikercityApp.Models.User
{
	public class DireccionModel : ExtendedBindableObject
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

		private bool _isActive;

		public bool IsActive
		{
			get { return _isActive; }
			set {
				_isActive = value;
				if (IsActive)
					SelectedIcon = "dot-circle";
				else
					SelectedIcon = "circle";
				if (IsActive)
					SelectedIconColor = Color.PaleVioletRed;
				else
					SelectedIconColor = Color.Gray;

				RaisePropertyChanged(() => IsActive); }
		}


		private string _selectedIcon;
		public string SelectedIcon
		{
			get
			{
				if (IsActive)
					_selectedIcon = "dot-circle";
				else
					_selectedIcon = "circle";
				return _selectedIcon;
			}
			set
			{
				_selectedIcon = value;
				RaisePropertyChanged(() => SelectedIcon);
			}
		}
		private Color _selectedIconColor;
		public Color SelectedIconColor
		{
			get
			{
				if (IsActive)
					_selectedIconColor = Color.PaleVioletRed;
				else
					_selectedIconColor = Color.Gray;
				return _selectedIconColor;
			}
			set
			{
				_selectedIconColor = value;
				RaisePropertyChanged(() => SelectedIconColor);
			}
		}


		public string DireccionConcatenada
		{
			get
			{
				string ret = direccion + " " + lote + ", " + urbanizacion;
				return ret;
			}
		}

		public string NombresApellidos
		{
			get { return nombres + " " + apellidos; }
		}
	}
}
