using BikercityApp.Extensions;
using BikercityApp.Models.Catalogo;
using BikercityApp.Services.Catalogo;
using BikercityApp.Services.User;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.User;
using System.Linq;
using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Catalogo
{
    public class CatalogoViewModel : ViewModelTab
	{
        private ICatalogoService _catalogoService;

        public CatalogoViewModel(
                                ICatalogoService catalogoService
                             )
        {
            _catalogoService = catalogoService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
			IsBusy = true;
			try
			{
				await base.InitializeAsync(navigationData);
				HomeColor = p_selectedColor;
				List<DetalleProductoModel> detalleProductos = await _catalogoService.GetDetalleProductoLista();
				ProductoList = new FlowObservableCollection<DetalleProductoModel>(detalleProductos);
				List<TipoProductoModel> tipoProductos = await _catalogoService.GetAllTipoProducto();
				TipoProductoList = new FlowObservableCollection<TipoProductoModel>(tipoProductos);
			}
			catch (Exception ex)
			{
			}
			IsBusy = false;
		}


		private string _buscadorList;

        public string BuscadorList
        {
            get { return _buscadorList; }
            set { _buscadorList = value;
                if (value != null) {

                     Task.Run(async () => {
                         List<DetalleProductoModel> detalleProductos = await _catalogoService.GetDetalleProductoLista();
                         ProductoList = new FlowObservableCollection<DetalleProductoModel>(detalleProductos.Where(l => l.titulo.Contains(value)).ToObservableCollection());
                     });
                }
                RaisePropertyChanged(() => BuscadorList);
            }
        }



        public ICommand OrdenarTodoCommand => new Command(async () => await OrdenarTodo());

        private async Task OrdenarTodo()
        {
            List<DetalleProductoModel> detalleProductos = await _catalogoService.GetDetalleProductoLista();

            ProductoList = new FlowObservableCollection<DetalleProductoModel>(detalleProductos);
        }

        public ICommand OrdernarMenorMayorCommand => new Command(async () => await OrdernarMenorMayor());

        private async Task OrdernarMenorMayor()
        {
            List<DetalleProductoModel> detalleProductos = await _catalogoService.GetDetalleProductoLista();
            
            ProductoList = new FlowObservableCollection<DetalleProductoModel>(detalleProductos.OrderBy(l => l.precioVenta).ToObservableCollection());
        }

        public ICommand OrdernarMayorMenorCommand => new Command(async () => await OrdernarMayorMenor());

        private async Task OrdernarMayorMenor()
        {
            List<DetalleProductoModel> detalleProductos = await _catalogoService.GetDetalleProductoLista();

            ProductoList = new FlowObservableCollection<DetalleProductoModel>(detalleProductos.OrderByDescending(l => l.precioVenta).ToObservableCollection());

        }

        public ICommand OrdernarTipoProductoCommand => new Command<TipoProductoModel>(async (tipo) => await OrdernarTipoProducto(tipo));

        private async Task OrdernarTipoProducto(TipoProductoModel model)
        {
            List<DetalleProductoModel> detalleProductos = await _catalogoService.GetDetalleProductoLista();

            ProductoList = new FlowObservableCollection<DetalleProductoModel>(detalleProductos.Where(l=>l.idTipoProducto==model.idTipoProducto).ToObservableCollection());

        }

            

        public ICommand GoToDetalleViewCommand => new Command<DetalleProductoModel>(async (detalle) => await GoToDetalleView(detalle));

        private async Task GoToDetalleView(DetalleProductoModel model)
        {
            await NavigationService.NavigateToAsync<DetalleProductoViewModel>(model);
        }



        private FlowObservableCollection<DetalleProductoModel> productoList;
        
        public FlowObservableCollection<DetalleProductoModel> ProductoList
        {
            get { return productoList; }
            set
            {
                productoList = value;
                RaisePropertyChanged(() => ProductoList);
            }
        }

        private FlowObservableCollection<TipoProductoModel> tipoproductoList;

        public FlowObservableCollection<TipoProductoModel> TipoProductoList
        {
            get { return tipoproductoList; }
            set
            {
                tipoproductoList = value;
                RaisePropertyChanged(() => TipoProductoList);
            }
        }

    }
}
