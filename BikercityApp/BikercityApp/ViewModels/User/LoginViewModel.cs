using BikercityApp.Helpers;
using BikercityApp.Models.User;
using BikercityApp.Models.User.ModeloServicio;
using BikercityApp.Services.Cliente;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.Catalogo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.User
{
	public class LoginViewModel : ViewModelBase
	{


		public LoginViewModel(
								IClienteService clienteService
							 )
		{
			_clienteService = clienteService;
		}

		private IClienteService _clienteService;



		private string _correoCliente;

		public string correoCliente
		{
			get { return _correoCliente; }
			set
			{
				_correoCliente = value;
				RaisePropertyChanged(() => correoCliente);
			}
		}
		private string _contraseñaCliente = "";

		public string contraseñaCliente
		{
			get { return _contraseñaCliente; }
			set
			{
				_contraseñaCliente = value;
				RaisePropertyChanged(() => contraseñaCliente);
			}
		}


		private ObservableCollection<UserModel> userList;

		public ObservableCollection<UserModel> UserList
		{
			get { return userList; }
			set
			{
				userList = value;
				RaisePropertyChanged(() => UserList);
			}
		}

		public ICommand IniciarSesionCommand => new Command(async () => await IniciarSesion());
		private async Task IniciarSesion()
		{
			try
			{
                LoginClienteInput clienteInput = new LoginClienteInput();
                clienteInput.correo = correoCliente;
                clienteInput.contrasena = contraseñaCliente;
                var response = await _clienteService.LoginCliente(clienteInput);
                if (response.result)
                {
                    Settings.idCliente = response.id;
                    Settings.nombreCliente = response.nombre;
                    Settings.paternoCliente = response.paterno;
                    Settings.maternoCliente = response.materno;
                    Settings.loggedIn = true;

                    Settings.ShoppingCartList = new List<Models.Catalogo.DetalleProductoModel>();
					await NotificationHelper.registerToken(Settings.FCMToken);

                    if (typeFromNavigation == null)
                    {
                        await NavigationService.NavigateToAsync<CatalogoViewModel>();
                    }
                    else
                    {
                        await NavigationService.NavigateToAsync(typeFromNavigation, null);
                    }
                }
                else
                {
                    //mostrar mensaje usuario
                }

            }
			catch (Exception ex)
			{

			}
			
		}

        public ICommand GoToRegistrarUsuarioViewCommand => new Command(async () => await GoToRegistrarUsuarioView());
        private async Task GoToRegistrarUsuarioView()
        {
            await NavigationService.NavigateToAsync<RegistrarUsuarioViewModel>();
        }

        public override async Task InitializeAsync(object navigationData)
		{
			try
			{
				typeFromNavigation = navigationData as Type;
			}
			catch (Exception ex)
			{

			}
		}
		private Type typeFromNavigation;



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




	}
}
