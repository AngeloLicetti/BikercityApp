
using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Orden;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Ordenes
{
    public class HistorialCompraViewModel : ViewModelBase
    {
        private IOrdenService _ordenService;

        public HistorialCompraViewModel(
                                IOrdenService ordenService
                             )
        {
            _ordenService = ordenService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await base.InitializeAsync(navigationData);
                List<OrdenModel> listOrden = await _ordenService.GetOrdenByCliente(Settings.idCliente);
                OrdenList = listOrden.ToObservableCollection();
            }
            catch (Exception ex)
            {
            }
            
        }
        //GoToDetalleCompraViewCommand
        public ICommand GoToDetalleCompraViewCommand => new Command<OrdenModel>(async (detalle) => await GoToDetalleCompraView(detalle));

        private async Task GoToDetalleCompraView(OrdenModel model)
        {
            await NavigationService.NavigateToAsync<DetalleCompraViewModel>(model);
        }

        private ObservableCollection<OrdenModel> _ordenList;

        public ObservableCollection<OrdenModel> OrdenList
        {
            get { return _ordenList; }
            set
            {
                _ordenList = value;
                RaisePropertyChanged(() => OrdenList);
            }
        }

    }
}
