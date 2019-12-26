using BikercityApp.Models.Catalogo.ModeloServicio;
using BikercityApp.Services.Tarjeta;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.User;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Catalogo
{
	public class PagoViewModel : ViewModelBase
	{
		ITarjetaService _tarjetaService;

		public PagoViewModel(ITarjetaService tarjetaService)
		{
			_tarjetaService = tarjetaService;
		}


		private TarjetaModel _tarjetaPrede;

		public TarjetaModel TarjetaPrede
		{
			get { return _tarjetaPrede; }
			set
			{
				_tarjetaPrede = value;
				RaisePropertyChanged(() => _tarjetaPrede);
			}
		}

		private string _tarjeta;

		public string Tarjeta
		{
			get { return _tarjeta; }
			set
			{
				_tarjeta = value;
				RaisePropertyChanged(() => Tarjeta);
			}
		}
		private string _nombresApellidos;
		public string NombresApellidos
		{
			get { return _nombresApellidos; }
			set
			{
				_nombresApellidos = value;
				RaisePropertyChanged(() => NombresApellidos);
			}
		}
		private string _direccion;

		public string Direccion
		{
			get { return _direccion; }
			set
			{
				_direccion = value;
				RaisePropertyChanged(() => Direccion);
			}
		}

		public string _caducidad;

		public string Caducidad
		{
			get { return _caducidad; }
			set
			{
				_caducidad = value;
				RaisePropertyChanged(() => Caducidad);
			}
		}

	//	  private async Task ActualizarTarjetaPredeterminada()
	//	 {
		//   var listaTarjetaPre = await _tarjetaService.getTarjetaPredeterminada();
 //		     if (listaTarjetaPre.Count == 0)
		//   {
	//	     PagoButtonText = "Agregar nuevo metodo de pago";
//		 }
	//	 else
	//  {
		//mostrar la direccion elegida en ConfirmarPedidoView:
	  // TarjetaPrede = listaTarjetaPre[0];
       //         Tarjeta = TarjetaPrede.numeroTarjeta;
//		    NombresApellidos = TarjetaPrede.nombre + " " + TarjetaPrede.apellidos;
	//   Direccion = TarjetaPrede.direccion;
	//	  Caducidad = TarjetaPrede.mes + "/" + TarjetaPrede.año;
	//	 PagoButtonText = "Cambiar metodo de pago";
//		 }
	// }
		private string _tarjetaButtonText;
		public string PagoButtonText
		{
			get { return _tarjetaButtonText; }
			set { _tarjetaButtonText = value; RaisePropertyChanged(() => PagoButtonText); }
		}
		public ICommand ConsultarPagoCommand => new Command(async () => await ConsultarPago());

		private async Task ConsultarPago()
		{
			if (TarjetaPrede == null)
			{
				await NavigationService.NavigateToAsync<AgregarTarjetaViewModel>();
			}
			else
			{
				await NavigationService.NavigateToAsync<GestionarDireccionesViewModel>();
			}

		}
	}
}
