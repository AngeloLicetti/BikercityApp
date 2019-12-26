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
	public class HistorialMensajesViewModel : ViewModelTab
	{
		private IServicioService _ServicioService;
		private MensajeNavModel navModel;
		public HistorialMensajesViewModel(
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
				navModel = navigationData as MensajeNavModel;
				await ListarMensajes();
			}
			catch (Exception ex)
			{
				//error
			}

		}
		private async Task ListarMensajes()
		{
            try
            {
                List<MensajeSolicitudServicio> mensajeList;
                if (navModel.type == Constants.TypeMensajeNav.Cliente)
                {
                    mensajeList = await _ServicioService.GetMensajeCliente(navModel.id);
                    IsSend = false;
                }
                else
                {
                    mensajeList = await _ServicioService.GetMensajeSolicitudServicio(navModel.id);
                    IsSend = true;
                }
                MensajeList = mensajeList.ToObservableCollection();
            }
            catch (Exception ex)
            {
            }
			
		}
		private bool _isSend = false;

		public bool IsSend
		{
			get { return _isSend; }
			set { _isSend = value; RaisePropertyChanged(() => IsSend); }
		}

		private ObservableCollection<MensajeSolicitudServicio> _mensajeList;
		public ObservableCollection<MensajeSolicitudServicio> MensajeList
		{
			get { return _mensajeList; }
			set
			{
				_mensajeList = value;
				RaisePropertyChanged(() => MensajeList);
			}
		}
		private string _mensaje;

		public string Mensaje
		{
			get { return _mensaje; }
			set { _mensaje = value; RaisePropertyChanged(() => Mensaje); }
		}
		public ICommand SendCommand => new Command(async () => await Send());
		private async Task Send()
		{
			try
			{
				MensajeSolicitudServicio mensaje = new MensajeSolicitudServicio
				{
					IdCliente = Settings.idCliente,
					IdSolcitudServicio = navModel.id,
					Mensaje = Mensaje,
					Media = null
				};
				var response = await _ServicioService.AddMensajeSolicitudServicio(mensaje);
				if (response.result)
				{
					await ListarMensajes();
				}
				else
				{
					//Error
				}
			}
			catch (Exception ex)
			{
				//Error no controlado
			}
		}

		public ICommand NavigateBackCommand => new Command(async () => await NavigateBack());
		private async Task NavigateBack()
		{
			await NavigationService.NavigateBackAsync();
		}

	}
}
