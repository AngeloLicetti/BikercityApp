using BikercityApp.Helpers;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.Catalogo;
using BikercityApp.ViewModels.Ordenes;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.User
{
	public class PerfilViewModel : ViewModelTab
	{

		
		public override async Task InitializeAsync(object navigationData)
		{
            try
            {
                await base.InitializeAsync(navigationData);
                ProfileColor = p_selectedColor;
                NombreUsuario = Settings.nombreCliente + " " + Settings.paternoCliente;
                IsLogged = Settings.loggedIn;
            }
            catch (Exception ex)
            {
            }
			
		}
		private string _nombreUsuario;

		public string NombreUsuario
		{
			get { return _nombreUsuario; }
			set { _nombreUsuario = value; RaisePropertyChanged(() => NombreUsuario); }
		}
		private bool _isLogged;

		public bool IsLogged
		{
			get { return _isLogged; }
			set { _isLogged = value; RaisePropertyChanged(() => IsLogged); }
		}
        //

        public ICommand GoToHistorialCompraViewCommand => new Command(async () => await GoToHistorialCompraView());
        private async Task GoToHistorialCompraView()
        {
            await NavigationService.NavigateToAsync<HistorialCompraViewModel>();
        }
        //
        public ICommand GoToLoginViewCommand => new Command(async () => await GoToLoginView());
        private async Task GoToLoginView()
		{
			await NavigationService.NavigateToAsync<LoginViewModel>();
		}
        public ICommand GoToRegistrarUsuarioViewCommand => new Command(async () => await GoToRegistrarUsuarioView());
        private async Task GoToRegistrarUsuarioView()
        {
            await NavigationService.NavigateToAsync<RegistrarUsuarioViewModel>();
        }
        public ICommand CerrarSesionCommand => new Command(async () => await CerrarSesion());
		private async Task CerrarSesion()
		{
			Settings.idCliente = default(int);
			Settings.nombreCliente = default(string);
			Settings.paternoCliente = default(string);
			Settings.maternoCliente = default(string);
			Settings.loggedIn = false;
			await NavigationService.NavigateToAsync<CatalogoViewModel>();
		}


        public ICommand GoToHistorialServicioViewCommand => new Command(async () => await GoToHistorialServicioView());
        private async Task GoToHistorialServicioView()
        {
            await NavigationService.NavigateToAsync<HistorialServicioViewModel>();
        }

        public ICommand GoToPerfilUserViewCommand => new Command(async () => await GoToPerfilUserView());
        private async Task GoToPerfilUserView()
        {
            await NavigationService.NavigateToAsync<PerfilUserViewModel>();
        }


    }
}
