using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Catalogo.ModeloServicio;
using BikercityApp.Models.User.TarjetaServicio;
using BikercityApp.Services.Tarjeta;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.User
{
    public class GestionarMetodoPagoViewModel : ViewModelBase
    {
        private ITarjetaService _tarjetaService;
        private static GestionarMetodoPagoViewModel gestionarMetodoPagoViewModel;

        public GestionarMetodoPagoViewModel(ITarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await base.InitializeAsync(navigationData);
                ListaTarjetas = (await _tarjetaService.getListaTarjeta()).ToObservableCollection();

                if (gestionarMetodoPagoViewModel != null)
                {
                    MessagingCenter.Unsubscribe<MessageHelper, string>(gestionarMetodoPagoViewModel, MessageKeys.ListarTarjetasKey);
                }
                MessagingCenter.Subscribe<MessageHelper, string>(this, MessageKeys.ListarTarjetasKey, async (seder, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsBusy = true;
                        ListaTarjetas = (await _tarjetaService.getListaTarjeta()).ToObservableCollection();
                        IsBusy = false;
                    });
                });
            } catch (Exception ex)
            {

            }
        }

        private ObservableCollection<TarjetaModel> _listaTarjetas;

        public ObservableCollection<TarjetaModel> ListaTarjetas
        {
            get { return _listaTarjetas; }
            set
            {
                _listaTarjetas = value;
                RaisePropertyChanged(() => ListaTarjetas);
            }
        }

        public ICommand AgregarTarjetaCommand => new Command(async () => await AgregarTarjeta());

        private async Task AgregarTarjeta()
        {
            await NavigationService.NavigateToAsync<AgregarTarjetaViewModel>();
        }
    }
}

