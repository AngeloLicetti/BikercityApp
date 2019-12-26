using BikercityApp.Helpers;
using BikercityApp.Models.Servicios;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Servicios
{
    public class DetalleServicioViewModel : ViewModelBase
    {
        private IServicioService _servicioService;

        public DetalleServicioViewModel(
                                IServicioService servicioService
                             )
        {
            _servicioService = servicioService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                ServicioModel model = navigationData as ServicioModel;
                DetalleServicioModel detalle = await _servicioService.GetServicioByIdServicio(model.idServicio);

                DetalleServicio = detalle;

                if (Settings.idCliente > 0)
                {
                    IsSolicitar = true;
                }
                else
                {
                    IsSolicitar = false;
                }
            }
            catch (Exception ex)
            {
            }
            
        }
        private bool _isSolicitar = true;
        public bool IsSolicitar
        {
            get { return _isSolicitar; }
            set { _isSolicitar = value; RaisePropertyChanged(() => IsSolicitar); }
        }
        private DetalleServicioModel _detalleServicio;
        public DetalleServicioModel DetalleServicio
        {
            get => _detalleServicio;
            set { _detalleServicio = value; RaisePropertyChanged(() => DetalleServicio); }
        }

        public ICommand GoToSolicitarServicioViewCommand => new Command(async () => await GoToSolicitarServicioView());

        private async Task GoToSolicitarServicioView()
        {
            await NavigationService.NavigateToAsync<SolicitarServicioViewModel>(DetalleServicio);
        }
    }
}
