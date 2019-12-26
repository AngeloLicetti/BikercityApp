using System;
using System.Collections.Generic;
using System.Text;
using BikercityApp.Extensions;
using BikercityApp.Models.User;
using BikercityApp.Services.Direccion;
using BikercityApp.ViewModels.Base;
using System.Collections.ObjectModel;
using BikercityApp.Services.Tarjeta;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BikercityApp.Models.Catalogo.ModeloServicio;

namespace BikercityApp.ViewModels.User
{
    class TarjetaViewModel : ViewModelBase
    {
        private ITarjetaService _tarjetaService;

        public TarjetaViewModel(
                              ITarjetaService tarjetaService
                           )
        {
            _tarjetaService = tarjetaService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            var listaTarjeta = await _tarjetaService.getListaTarjeta();
            TarjetasLista = listaTarjeta.ToObservableCollection();
            //DireccionPredeterminada = await _direccionService.getDireccionPredeterminada();

        }

        private bool _IsUpdating = false;

        public bool IsUpdating
        {
            get { return _IsUpdating; }
            set { _IsUpdating = value; RaisePropertyChanged(() => IsUpdating); }
        }

        public TarjetaModel _tarjetaPredeterminada;

        public TarjetaModel TarjetaPredeterminada
        {
            get { return _tarjetaPredeterminada; }
            set { _tarjetaPredeterminada = value; RaisePropertyChanged(() => TarjetaPredeterminada); }
        }

        private ObservableCollection<TarjetaModel> _tarjetaLista;

        public ObservableCollection<TarjetaModel> TarjetasLista
        {
            get { return _tarjetaLista; }
            set
            {
                _tarjetaLista = value;
                RaisePropertyChanged(() => TarjetasLista);
            }
        }

      
    }
}

