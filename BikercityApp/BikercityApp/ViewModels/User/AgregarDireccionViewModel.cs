using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.User;
using BikercityApp.Extensions;
using BikercityApp.Services.Direccion;
using BikercityApp.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BikercityApp.ViewModels.Catalogo;
using BikercityApp.Helpers;
using BikercityApp.Models.User.DireccionServicio;

namespace BikercityApp.ViewModels.User
{
    public class AgregarDireccionViewModel : ViewModelBase
    {
        private IDireccionService _direccionService;

        public AgregarDireccionViewModel(IDireccionService direccionService)
        {
            _direccionService = direccionService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await base.InitializeAsync(navigationData);
            }
            catch (Exception ex)
            {
            }
        }

        private bool _IsUpdating = false;

        public bool IsUpdating
        {
            get { return _IsUpdating; }
            set { _IsUpdating = value; RaisePropertyChanged(() => IsUpdating); }
        }

        public ICommand AgregarDireccionCommand => new Command<DireccionModel>(async (item) => await AgregarDireccion(item));

        private async Task AgregarDireccion(DireccionModel item)
        {
            IsUpdating = true;
			try
			{
				DireccionModel direccionModel = new DireccionModel();
				direccionModel.direccion = Direccion;
				direccionModel.lote = Lote;
				direccionModel.urbanizacion = Urbanizacion;
				direccionModel.referencia = Referencia;
				//cambiar esto de isActive:
				direccionModel.IsActive = IsActive;
				direccionModel.distrito = Distrito;
				direccionModel.nombres = Settings.nombreCliente;
				direccionModel.apellidos = Settings.paternoCliente + " " + Settings.maternoCliente;
				direccionModel.email = Email;
				direccionModel.numeroTelefonico = NumeroTelefonico;
				direccionModel.idCliente = Settings.idCliente;
				var response = await _direccionService.agregarDireccion(direccionModel);
				MessageHelper.ListarDirecciones();
				await NavigationService.NavigateBackAsync();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                IsUpdating = false;
            }
            IsUpdating = false;
        }
		
		private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                RaisePropertyChanged(() => Direccion);
            }
        }

        private string _direccion;

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                _direccion = value;
                RaisePropertyChanged(() => Direccion);
            }
        }

        private string _lote;

        public string Lote
        {
            get { return _lote; }
            set
            {
                _lote = value;
                RaisePropertyChanged(() => Lote);
            }
        }

        private string _urbanizacion;

        public string Urbanizacion
        {
            get { return _urbanizacion; }
            set
            {
                _urbanizacion = value;
                RaisePropertyChanged(() => Urbanizacion);
            }
        }

        private string _referencia;

        public string Referencia
        {
            get { return _referencia; }
            set
            {
                _referencia = value;
                RaisePropertyChanged(() => Referencia);
            }
        }

        private string _distrito;

        public string Distrito
        {
            get { return _distrito; }
            set
            {
                _distrito = value;
                RaisePropertyChanged(() => Distrito);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        private string _numeroTelefonico;

        public string NumeroTelefonico
        {
            get { return _numeroTelefonico; }
            set
            {
                _numeroTelefonico = value;
                RaisePropertyChanged(() => NumeroTelefonico);
            }
        }
    }
}
