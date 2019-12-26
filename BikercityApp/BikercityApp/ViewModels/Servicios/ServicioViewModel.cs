using BikercityApp.Extensions;
using BikercityApp.Models.Servicios;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Servicios
{
    public class ServicioViewModel : ViewModelTab
	{

        private IServicioService _servicioService;

        public ServicioViewModel(
                                IServicioService servicioService
                             )
        {
            _servicioService = servicioService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await base.InitializeAsync(navigationData);
                ServiceColor = p_selectedColor;
                List<ServicioModel> servicios = await _servicioService.GetAllServicios();
                ServicioList = new ObservableCollection<ServicioModel>();
                ServicioList = servicios.Where(l => l.isActive == true).ToObservableCollection();
            }
            catch (Exception ex)
            {
            }
           
        }

        private ObservableCollection<ServicioModel> _servicioList;
        public ObservableCollection<ServicioModel> ServicioList
        {
            get { return _servicioList; }
            set
            {
                _servicioList = value;
                RaisePropertyChanged(() => ServicioList);
            }
        }
        public ICommand GoToDetalleServicioViewCommand => new Command<ServicioModel>(async (detalle) => await GoToDetalleServicioView(detalle));

        private async Task GoToDetalleServicioView(ServicioModel model)
        {
            await NavigationService.NavigateToAsync<DetalleServicioViewModel>(model);
        }
    }
}
