using BikercityApp.ViewModels.Base;
using Xamarin.Forms;

namespace BikercityApp.Models.Catalogo.ModeloServicio
{
	public class TarjetaModel : ExtendedBindableObject
	{
		public int idTarjeta { get; set; }
		public string nombre { get; set; }
		public string apellidos { get; set; }
		public string direccion { get; set; }
		public int mes { get; set; }
		public int año { get; set; }
		public int cvv { get; set; }
		public int idCliente { get; set; }

		private bool _isActive;

		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				if (IsActive)
					SelectedIcon = "dot-circle";
				else
					SelectedIcon = "circle";
				if (IsActive)
					SelectedIconColor = Color.PaleVioletRed;
				else
					SelectedIconColor = Color.Gray;

				RaisePropertyChanged(() => IsActive);
			}
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

		private string _numeroTarjeta;

		public string NumeroTarjeta
		{
			get { return _numeroTarjeta; }
			set
			{
				_numeroTarjeta = value;
				string nroTarjeta = "";
				nroTarjeta = _numeroTarjeta.Substring(11, 4);
				NumeroTarjetaFormat = "**** **** **** " + nroTarjeta;
				RaisePropertyChanged(() => NumeroTarjeta);
			}
		}


		private string _numeroTarjetaFormat;
		public string NumeroTarjetaFormat
		{
			get { return _numeroTarjetaFormat; }
			set { _numeroTarjetaFormat = value; RaisePropertyChanged(() => NumeroTarjetaFormat); }
		}

		public string NombresApellidos
		{
			get { return nombre + " " + apellidos; }
		}
	}
}

