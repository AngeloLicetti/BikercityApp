using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Catalogo.ResenaServicio;
using BikercityApp.Services.Carrito;
using BikercityApp.Services.Catalogo;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Catalogo
{
    public class DetalleProductoViewModel : ViewModelBase
    {
        private ICatalogoService _catalogoService;
        private ICarritoService _carritoService;
        private IOrdenService _ordenService;
        List<ComentarioModel> resenalist;

        public DetalleProductoViewModel(
                                ICatalogoService catalogoService,
                                ICarritoService carritoService,
                                IOrdenService ordenService
                             )
        {
            _catalogoService = catalogoService;
            _carritoService = carritoService;
            _ordenService = ordenService;

        }
        //VerifyDetalleOrdenByIdCliente
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                DetalleProductoModel model = navigationData as DetalleProductoModel;
                DetalleProducto = model;
                model = await _catalogoService.GetDetalleProductoById(model.idDetalleProducto);
                DetalleProducto = model;
                bool Comprado = await _ordenService.VerifyDetalleOrdenByIdCliente(Settings.idCliente, DetalleProducto.idDetalleProducto);

                IsStock = DetalleProducto.stockWeb > 0;
                resenalist = await _catalogoService.GetComentarioByID(model.idDetalleProducto);
                ResenaTotal = resenalist.Count;
                ResenasList = resenalist.Take(2).ToObservableCollection();
                if (resenalist.Count > 0)
                    CalificacionTotal = resenalist.Average(r => r.calificacion);
                else
                    CalificacionTotal = 0;
                if (resenalist.Count >= 1)
                    VerMasValoraciones = true;
                else
                    VerMasValoraciones = false;
                if (Settings.idCliente > 0) IsLogged = true;
                else IsLogged = false;

                if (IsLogged)
                {
                    if (!Comprado)
                        Validacion = true;
                    if (ResenasList.Count(l => l.idCliente == Settings.idCliente) < 2)
                        Validacion = true;
                    else Validacion = false;

                }
                else Validacion = false;
            }
            catch (Exception ex)
            {
            }

        }
        private int _resenaTotal;

        public int ResenaTotal
        {
            get { return _resenaTotal; }
            set { _resenaTotal = value; RaisePropertyChanged(() => ResenaTotal); }
        }

        private bool _isLogged;
        public bool IsLogged
        {
            get { return _isLogged; }
            set
            {
                _isLogged = value;
                RaisePropertyChanged(() => IsLogged);
            }
        }



        private bool _verMasValoraciones;
        public bool VerMasValoraciones
        {
            get { return _verMasValoraciones; }
            set
            {
                _verMasValoraciones = value;
                RaisePropertyChanged(() => VerMasValoraciones);
            }
        }
        private bool _isStock;

        public bool IsStock
        {
            get { return _isStock; }
            set { _isStock = value; RaisePropertyChanged(() => IsStock); }
        }

        private double _calificacionTotal;
        public double CalificacionTotal
        {
            get { return _calificacionTotal; }
            set
            {
                _calificacionTotal = value;
                RaisePropertyChanged(() => CalificacionTotal);
            }
        }

        private int _calificacionRegistro;
        public int CalificacionRegistro
        {
            get { return _calificacionRegistro; }
            set
            {
                _calificacionRegistro = value;
                RaisePropertyChanged(() => CalificacionRegistro);
            }
        }

        private string _descripcionRegistro;
        public string DescripcionRegistro
        {
            get { return _descripcionRegistro; }
            set
            {
                _descripcionRegistro = value;
                RaisePropertyChanged(() => DescripcionRegistro);
            }
        }

        private bool _validacion;
        public bool Validacion
        {
            get { return _validacion; }
            set
            {
                _validacion = value;
                RaisePropertyChanged(() => Validacion);
            }
        }

        private ObservableCollection<ComentarioModel> _resenasList;
        public ObservableCollection<ComentarioModel> ResenasList
        {
            get { return _resenasList; }
            set
            {
                _resenasList = value;
                RaisePropertyChanged(() => ResenasList);
            }
        }

        private DetalleProductoModel _detalleProducto;

        public DetalleProductoModel DetalleProducto
        {
            get { return _detalleProducto; }
            set { _detalleProducto = value; RaisePropertyChanged(() => DetalleProducto); }
        }

        public ICommand RegistrarResenaCommand => new Command(async () => await RegistrarResena());
        private async Task RegistrarResena()
        {
            try
            {
                CrearResenaInputModel resenaInput = new CrearResenaInputModel();
                resenaInput.descripcion = DescripcionRegistro;
                resenaInput.calificacion = CalificacionRegistro;
                resenaInput.idDetalleProducto = DetalleProducto.idDetalleProducto;
                resenaInput.idCliente = Settings.idCliente;

                var response = await _catalogoService.CrearResena(resenaInput);
                if (response.result)
                {
                    CalificacionRegistro = 0;
                    DescripcionRegistro = String.Empty;
                    Validacion = false;
                    ActualizarReseñas();

                }
                else
                {
                    //IsModalVisible = true;
                    //TextoInformacion = response.message;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private async Task ActualizarReseñas()
        {
            try
            {
                resenalist = await _catalogoService.GetComentarioByID(DetalleProducto.idDetalleProducto);
                ResenaTotal = resenalist.Count;
                ResenasList = resenalist.Take(2).ToObservableCollection();
                if (resenalist.Count > 0)
                    CalificacionTotal = resenalist.Average(r => r.calificacion);
                else
                    CalificacionTotal = 0;
                if (resenalist.Count >= 1)
                    VerMasValoraciones = true;
                else
                    VerMasValoraciones = false;
            }
            catch (Exception ex)
            {
            }
            
        }

        public ICommand GoToPerfilViewCommand => new Command(async () => await GoToPerfilView());

        private async Task GoToPerfilView()
        {
            await NavigationService.NavigateToAsync<PerfilViewModel>();
        }
        public ICommand GoToCatalogoViewCommand => new Command(async () => await GoToCatalogoView());

        private async Task GoToCatalogoView()
        {
            await NavigationService.NavigateToAsync<CatalogoViewModel>();
        }
        public ICommand agregarCarritoCommand => new Command(async () => await agregarCarrito());
        private async Task agregarCarrito()
        {
            await _carritoService.agregarCarrito(DetalleProducto);
            await NavigationService.NavigateToAsync<CarritoViewModel>();
        }
        public ICommand eliminarCarritoCommand => new Command(async () => await eliminarCarrito());
        private async Task eliminarCarrito()
        {
            await _carritoService.eliminarCarrito(DetalleProducto);
            await NavigationService.NavigateToAsync<CarritoViewModel>();
        }
        public ICommand GoToValoracionesViewCommand => new Command(async () => await GoToValoracionesView());

        private async Task GoToValoracionesView()
        {
            await NavigationService.NavigateToAsync<ValoracionesViewModel>(resenalist);
        }
    }
}
