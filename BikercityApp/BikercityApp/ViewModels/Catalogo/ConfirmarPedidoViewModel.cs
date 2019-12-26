using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Catalogo.ModeloServicio;
using BikercityApp.Models.Orden;
using BikercityApp.Models.User;
using BikercityApp.Services.Carrito;
using BikercityApp.Services.Direccion;
using BikercityApp.Services.Ordenes;
using BikercityApp.Services.Tarjeta;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.User;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Catalogo
{
	public class ConfirmarPedidoViewModel : ViewModelBase
	{
		private IDireccionService _direccionService;
		private ICarritoService _carritoService;
		private ITarjetaService _tarjetaService;
		private IOrdenService _ordenService;
		private static ConfirmarPedidoViewModel confirmarPedidoViewModel;

		public ConfirmarPedidoViewModel(IDireccionService direccionService, ICarritoService carritoService, 
										ITarjetaService tarjetaService, IOrdenService ordenService)
		{
			_direccionService = direccionService;
			_carritoService = carritoService;
			_tarjetaService = tarjetaService;
			_ordenService = ordenService;
		}

		public override async Task InitializeAsync(object navigationData)
		{
            try
            {
                IsBusy = true;
                await base.InitializeAsync(navigationData);
                await ActualizarDireccionPredeterminada();

                await ActualizarCarrito();
                await ActualizarTarjetaPredeterminada();

                if (confirmarPedidoViewModel != null)
                {
                    MessagingCenter.Unsubscribe<MessageHelper, string>(confirmarPedidoViewModel, MessageKeys.DireccionPredeterminadaKey);
                }
                MessagingCenter.Subscribe<MessageHelper, string>(this, MessageKeys.DireccionPredeterminadaKey, async (seder, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsBusy = true;
                        await ActualizarDireccionPredeterminada();
                        IsBusy = false;
                    });
                });
                if (confirmarPedidoViewModel != null)
                {
                    MessagingCenter.Unsubscribe<MessageHelper, string>(confirmarPedidoViewModel, MessageKeys.TarjetaPredeterminadaKey);
                }
                MessagingCenter.Subscribe<MessageHelper, string>(this, MessageKeys.TarjetaPredeterminadaKey, async (seder, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsBusy = true;
                        await ActualizarTarjetaPredeterminada();
                        IsBusy = false;
                    });
                });
                confirmarPedidoViewModel = this;

                IsBusy = false;
            }
            catch (Exception e)
            {
            }
			
		}


		private async Task ActualizarCarrito()
		{
			var listaCarrito = await _carritoService.getListaCarrito();
			CarritoLista = listaCarrito.ToObservableCollection();
		}
		private double _totalPrecio;

		public double TotalPrecio
		{
			get { return _totalPrecio; }
			set { _totalPrecio = value; RaisePropertyChanged(() => TotalPrecio); }
		}
		private bool _IsUpdating = false;

		public bool IsUpdating
		{
			get { return _IsUpdating; }
			set { _IsUpdating = value; RaisePropertyChanged(() => IsUpdating); }
		}

		private ObservableCollection<DetalleProductoModel> _carritoLista;

		public ObservableCollection<DetalleProductoModel> CarritoLista
		{
			get { return _carritoLista; }
			set
			{
				_carritoLista = value;

				if (value != null)
				{
					TotalPrecio = _carritoLista.Sum(d => d.precioVenta * d.Cantidad);
				}
				RaisePropertyChanged(() => CarritoLista);
			}
		}



		private DireccionModel _direccionPredeterminada;

		public DireccionModel DireccionPredeterminada
		{
			get { return _direccionPredeterminada; }
			set
			{
				_direccionPredeterminada = value;
				RaisePropertyChanged(() => DireccionPredeterminada);
			}
		}
		private TarjetaModel _tarjetaPredeterminada;

		public TarjetaModel TarjetaPredeterminada
		{
			get { return _tarjetaPredeterminada; }
			set
			{
				_tarjetaPredeterminada = value;
				RaisePropertyChanged(() => TarjetaPredeterminada);
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

		private string _numeroTelefonico;

		public string NumeroTelefonico
		{
			get { return _numeroTelefonico; }
			set
			{
				_numeroTelefonico = value;
				RaisePropertyChanged(() => NumeroTelefonico);
			}
		}

		private string _email;

		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				RaisePropertyChanged(() => Email);
			}
		}

		private async Task ActualizarDireccionPredeterminada()
		{
			var listaDireccionPredeterinada = await _direccionService.getDireccionPredeterminada();
			if (listaDireccionPredeterinada.Count != 0)
			{
				//mostrar la direccion elegida en ConfirmarPedidoView:
				DireccionPredeterminada = listaDireccionPredeterinada[0];
				Direccion = DireccionPredeterminada.direccion + " "
					+ DireccionPredeterminada.lote + ", " + DireccionPredeterminada.distrito
					+ DireccionPredeterminada.urbanizacion + ", " + DireccionPredeterminada.referencia;
				NombresApellidos = DireccionPredeterminada.nombres + " " + DireccionPredeterminada.apellidos;
				Email = DireccionPredeterminada.email;
				NumeroTelefonico = DireccionPredeterminada.numeroTelefonico;
				DireccionButtonText = "Cambiar dirección de entrega";
			}
		}

		private async Task ActualizarTarjetaPredeterminada()
		{
			var tarjetaPredeterminada = await _tarjetaService.getTarjetaPredeterminada();

			//mostrar la tarjeta elegida en ConfirmarPedidoView:
			TarjetaPredeterminada = tarjetaPredeterminada;
			NumeroTarjeta = TarjetaPredeterminada.NumeroTarjeta;
			
		}

		private string _direccionButtonText;
		public string DireccionButtonText
		{
			get { return _direccionButtonText; }
			set { _direccionButtonText = value; RaisePropertyChanged(() => DireccionButtonText); }
		}

		public ICommand GestionarDireccionCommand => new Command(async () => await GestionarDireccion());
		private async Task GestionarDireccion()
		{
			await NavigationService.NavigateToAsync<GestionarDireccionesViewModel>();
		}

		public ICommand GestionarTarjetaCommand => new Command(async () => await GestionarTarjeta());
		private async Task GestionarTarjeta()
		{
			await NavigationService.NavigateToAsync<GestionarMetodoPagoViewModel>();
		}

		public ICommand ComprarCommand => new Command(async () => await Comprar());
		private async Task Comprar()
		{
			TarjetaCreateInputModel tarjetaInputModel = new TarjetaCreateInputModel()
			{
				numeroTajeta = TarjetaPredeterminada.NumeroTarjeta,
				apellidos = TarjetaPredeterminada.apellidos,
				año = TarjetaPredeterminada.año,
				cvv = TarjetaPredeterminada.cvv,
				idCliente = Settings.idCliente,
				direccion = TarjetaPredeterminada.direccion,
				mes = TarjetaPredeterminada.mes,
				nombre = TarjetaPredeterminada.nombre
			};
			DireccionCreateInputModel direccionInputModel = new DireccionCreateInputModel()
			{
				apellidos = DireccionPredeterminada.apellidos,
				direccion = DireccionPredeterminada.direccion,
				distrito = DireccionPredeterminada.distrito,
				email = DireccionPredeterminada.email,
				idCliente = DireccionPredeterminada.idCliente,
				isActive = false,
				nombres = DireccionPredeterminada.nombres,
				numeroTelefonico = DireccionPredeterminada.numeroTelefonico,
				referencia = DireccionPredeterminada.referencia,
				lote = DireccionPredeterminada.lote,
				urbanizacion = DireccionPredeterminada.urbanizacion,
			};
			CreateOrdenInputModel inputModel = new CreateOrdenInputModel()
			{
				isSaveTarjeta = false,
				idCliente = Settings.idCliente,
				montototal = TotalPrecio,
				direccion = direccionInputModel,
				tarjeta = tarjetaInputModel
			};

			var response = await _ordenService.CreateOrden(inputModel);
			if(response.result)
			{
                try
                {
                    await NavigationService.NavigateToAsync<CatalogoViewModel>();
                    await NavigationService.RemoveBackStackAsync();
                }
                catch (Exception ex)
                {
                }
				
			}
			else
			{
				//controlarError
			}
		}
	}
}
