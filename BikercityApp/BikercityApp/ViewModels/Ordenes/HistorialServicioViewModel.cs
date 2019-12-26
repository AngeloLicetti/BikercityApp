using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Common;
using BikercityApp.Models.Servicios;
using BikercityApp.Models.Servicios.Navigation;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Ordenes
{
	public class HistorialServicioViewModel : ViewModelBase
	{

		private IServicioService _ServicioService;

		public HistorialServicioViewModel(
								IServicioService ServicioService
							 )
		{
			_ServicioService = ServicioService;
		}
		public override async Task InitializeAsync(object navigationData)
		{
			await base.InitializeAsync(navigationData);
			try
			{
				List<ServicioModel> listServicio = await _ServicioService.GetServiceByCliente(Settings.idCliente);
				SolicitudServicioList = listServicio.ToObservableCollection();
			}
			catch (Exception ex)
			{

			}

		}

		//GoToHistorialServicioDetalleViewCommand
		public ICommand GoToHistorialServicioDetalleViewCommand => new Command<ServicioModel>(async (detalle) => await GoToHistorialServicioDetalleView(detalle));

		private async Task GoToHistorialServicioDetalleView(ServicioModel model)
		{
			await NavigationService.NavigateToAsync<HistorialServicioDetalleViewModel>(model);
		}


		private ObservableCollection<ServicioModel> _solicitudServicioList;
		public ObservableCollection<ServicioModel> SolicitudServicioList
		{
			get { return _solicitudServicioList; }
			set
			{
				_solicitudServicioList = value;
				RaisePropertyChanged(() => SolicitudServicioList);
			}
		}

		public ICommand GoToCalendarCommand => new Command(async () => await GoToCalendar());
		private async Task GoToCalendar()
		{
			await NavigationService.NavigateToAsync<HistorialCalendarViewModel>();
		}
		public ICommand GoToMensajeCommand => new Command(async () => await GoToMensaje());
		private async Task GoToMensaje()
		{
			await NavigationService.NavigateToAsync<HistorialMensajesViewModel>(new MensajeNavModel
			{
				id = Settings.idCliente,
				type = Constants.TypeMensajeNav.Cliente
			});
		}
	}
}
