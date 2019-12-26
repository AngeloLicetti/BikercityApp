using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.User;
using BikercityApp.Models.User.DireccionServicio;
using BikercityApp.Services.Direccion;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.User
{
	public class GestionarDireccionesViewModel : ViewModelBase
	{
		private IDireccionService _direccionService;
		private static GestionarDireccionesViewModel gestionarDireccionesViewModel;

		public GestionarDireccionesViewModel(IDireccionService direccionService)
		{
			_direccionService = direccionService;
		}

		public override async Task InitializeAsync(object navigationData)
		{
            try
            {
                await base.InitializeAsync(navigationData);
                ListaDirecciones = (await _direccionService.getListaDirecciones()).ToObservableCollection();

                if (gestionarDireccionesViewModel != null)
                {
                    MessagingCenter.Unsubscribe<MessageHelper, string>(gestionarDireccionesViewModel, MessageKeys.ListarDireccionesKey);
                }
                MessagingCenter.Subscribe<MessageHelper, string>(this, MessageKeys.ListarDireccionesKey, async (seder, args) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsBusy = true;
                        ListaDirecciones = (await _direccionService.getListaDirecciones()).ToObservableCollection();
                        IsBusy = false;
                    });
                });
            }
            catch (Exception ex)
            {
            }
			
		}

		private ObservableCollection<DireccionModel> _listaDirecciones;

		public ObservableCollection<DireccionModel> ListaDirecciones
		{
			get { return _listaDirecciones; }
			set
			{
				_listaDirecciones = value;
				RaisePropertyChanged(() => ListaDirecciones);
			}
		}

		public ICommand AgregarDireccionCommand => new Command(async () => await AgregarDireccion());

		private async Task AgregarDireccion()
		{
			await NavigationService.NavigateToAsync<AgregarDireccionViewModel>();
		}
		public ICommand EliminarDireccionCommand => new Command<DireccionModel>(async (item) => await EliminarDireccion(item));
		private async Task EliminarDireccion(DireccionModel item)
		{
			try
			{
				EliminarDireccionInputModel inputModel = new EliminarDireccionInputModel()
				{
					idCliente = Settings.idCliente,
					idDireccion = item.idDireccion
				};
				var response = await _direccionService.eliminarDireccion(inputModel);
				if (response.result)
				{
					ListaDirecciones.Remove(item);
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			catch (Exception ex)
			{

			}
			finally
			{
			}
		}

		public ICommand DeterminarDireccionCommand => new Command<DireccionModel>(async (item) => await DeterminarDireccion(item));
		private async Task DeterminarDireccion(DireccionModel item)
		{
			try
			{
				PredeterminarDireccionInputModel inputModel = new PredeterminarDireccionInputModel()
				{
					idCliente = Settings.idCliente,
					idDireccion = item.idDireccion
				};
				var response = await _direccionService.predeterminarDireccion(inputModel);
				if (response.result)
				{
					ListaDirecciones.Remove(item);
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			catch (Exception ex)
			{

			}
			finally
			{
			}
			MessageHelper.PredeterminarDireccionResumen();
			await NavigationService.NavigateBackAsync();
		}
	}
}
