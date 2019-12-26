using BikercityApp.Extensions;
using BikercityApp.Models.User;
using BikercityApp.Services.Direccion;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.User
{
    class DireccionViewModel: ViewModelBase
    {
        private IDireccionService _direccionService;

        public DireccionViewModel(
                              IDireccionService direccionService
                           )
        {
            _direccionService = direccionService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                var listaDirecciones = await _direccionService.getListaDirecciones();
                DireccionesLista = listaDirecciones.ToObservableCollection();
            }
            catch (Exception ex)
            {
            }
            
            //DireccionPredeterminada = await _direccionService.getDireccionPredeterminada();

        }

        private bool _IsUpdating = false;

        public bool IsUpdating
        {
            get { return _IsUpdating; }
            set { _IsUpdating = value; RaisePropertyChanged(() => IsUpdating); }
        }

        public DireccionModel _direccionPredeterminada;

        public DireccionModel DireccionPredeterminada
        {
            get { return _direccionPredeterminada; }
            set { _direccionPredeterminada = value; RaisePropertyChanged(() => DireccionPredeterminada); }
        }

        private ObservableCollection<DireccionModel> _direccionesLista;

        public ObservableCollection<DireccionModel> DireccionesLista
        {
            get { return _direccionesLista; }
            set
            {
                _direccionesLista = value;
                RaisePropertyChanged(() => DireccionesLista);
            }
        }

        public ICommand EliminarDireccionCommand => new Command<DireccionModel>(async (item) => await EliminarDireccion(item));
        private async Task EliminarDireccion(DireccionModel item)
        {
            IsUpdating = true;
            IsUpdating = false;
        }
    }
}
