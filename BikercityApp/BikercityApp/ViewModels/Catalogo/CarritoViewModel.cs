using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Catalogo;
using BikercityApp.Services.Carrito;
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
	public class CarritoViewModel : ViewModelTab
	{
		private ICarritoService _carritoService;

		public CarritoViewModel(
							  ICarritoService carritoService
						   )
		{
			_carritoService = carritoService;
		}

        //ValidateButton
        public override async Task InitializeAsync(object navigationData)
		{
            IsBusy = true;
            try
            {
                await base.InitializeAsync(navigationData);
                CartColor = p_selectedColor;
                await ActualizarCarrito();
                if (CarritoLista.Count > 0)
                    ValidateButton = true;
                else
                    ValidateButton = false;
            }
            catch (Exception ex)
            {
            }
            IsBusy = false;
        }
		private async Task ActualizarCarrito()
        {
            try
            {
                var listaCarrito = await _carritoService.getListaCarrito();
                CarritoLista = listaCarrito.ToObservableCollection();
                foreach (var item in CarritoLista)
                {
                    item.AumentarCarritoCommand = AumentarCarritoCommand;
                    item.DisminuirCarritoCommand = DisminuirCarritoCommand;
                    item.EliminarCarritoCommand = EliminarCarritoCommand;
                }
                if (CarritoLista.Count > 0)
                    ValidateButton = true;
                else
                    ValidateButton = false;
            }
            catch (Exception ex)
            {
            }
			
		}

        private bool _validateButton = false;

        public bool ValidateButton
        {
            get { return _validateButton; }
            set { _validateButton = value; RaisePropertyChanged(() => ValidateButton); }
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

		public ICommand ComprarCommand => new Command(async () => await Comprar());
		private async Task Comprar()
		{
			if (Settings.loggedIn)
			{
				if (CarritoLista.Count > 0)
				{
					await NavigationService.NavigateToAsync<ConfirmarPedidoViewModel>();
				}
				else
				{
					//no se puede comprar
				}
			}
			else
			{
				await NavigationService.NavigateToAsync<LoginViewModel>(this.GetType());
			}
		}
		public ICommand EliminarCarritoCommand => new Command<DetalleProductoModel>(async (item) => await EliminarCarrito(item));
		private async Task EliminarCarrito(DetalleProductoModel item)
		{
			IsUpdating = true;
			await _carritoService.eliminarCarrito(item);
			await ActualizarCarrito();
			IsUpdating = false;
		}
		public ICommand AumentarCarritoCommand => new Command<DetalleProductoModel>(async (item) => await AumentarCarrito(item));
		private async Task AumentarCarrito(DetalleProductoModel item)
		{
			IsUpdating = true;
			if (item.Cantidad < 99)
			{
				item.Cantidad++;
				await _carritoService.actualizarCantidadCarrito(item, item.Cantidad);
				await ActualizarCarrito();
			}
			IsUpdating = false;
		}
		public ICommand DisminuirCarritoCommand => new Command<DetalleProductoModel>(async (item) => await DisminuirCarrito(item));
		private async Task DisminuirCarrito(DetalleProductoModel item)
		{
			IsUpdating = true;
			if (item.Cantidad > 0)
			{
				item.Cantidad--;
				await _carritoService.actualizarCantidadCarrito(item, item.Cantidad);
				await ActualizarCarrito();
			}
			IsUpdating = false;
		}
	}
}
