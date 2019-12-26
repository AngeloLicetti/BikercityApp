using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BikercityApp.Models.User;
using BikercityApp.Models.User.ClienteServicio;
using BikercityApp.Services.Cliente;
using BikercityApp.ViewModels.Base;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.User
{
    public class RegistrarUsuarioViewModel : ViewModelBase
    {
        private IClienteService _clienteService;

        public RegistrarUsuarioViewModel(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            
        }

        private bool _IsUpdating = false;

        public bool IsUpdating
        {
            get { return _IsUpdating; }
            set { _IsUpdating = value; RaisePropertyChanged(() => IsUpdating); }
        }

        public ICommand RegistrarClienteCommand => new Command(async () => await RegistrarCliente());

        private async Task RegistrarCliente()
        {
            IsUpdating = true;
            try
            {
                if (Contraseña == ConfirmarContraseña)
                {
                    AgregarClienteInputModel inputModel = new AgregarClienteInputModel()
                    {
                        correo = Correo,
                        contrasena = Contraseña,
                        nombre = Nombre,
                        paterno = Paterno,
                        materno = Materno,
                        direccion = Direccion,
                        distrito = Distrito,
                        telef = Telefono
                    };

                    var response = await _clienteService.agregarCliente(inputModel);
                    await NavigationService.NavigateBackAsync();
                }
                else
                {
                    //mostrar mensaje de que las contras no coinciden
                    MensajeError = "Contraseñas no coinciden";
                }
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

        private string _mensajeError;

        public string MensajeError
        {
            get { return _mensajeError; }
            set
            {
                _mensajeError = value;
                RaisePropertyChanged(() => MensajeError);
            }
        }

        private string _correo;

        public string Correo
        {
            get { return _correo; }
            set
            {
                _correo = value;
                RaisePropertyChanged(() => Correo);
            }
        }

        private string _contraseña;

        public string Contraseña
        {
            get { return _contraseña; }
            set
            {
                _contraseña = value;
                RaisePropertyChanged(() => Contraseña);
            }
        }

        private string _confirmarContraseña;

        public string ConfirmarContraseña
        {
            get { return _confirmarContraseña; }
            set
            {
                _confirmarContraseña = value;
                RaisePropertyChanged(() => ConfirmarContraseña);
            }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                RaisePropertyChanged(() => Nombre);
            }
        }


        private string _paterno;

        public string Paterno
        {
            get { return _paterno; }
            set
            {
                _paterno = value;
                RaisePropertyChanged(() => Paterno);
            }
        }

        private string _materno;

        public string Materno
        {
            get { return _materno; }
            set
            {
                _materno = value;
                RaisePropertyChanged(() => Materno);
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

        private string _telefono;

        public string Telefono
        {
            get { return _telefono; }
            set
            {
                _telefono = value;
                RaisePropertyChanged(() => Telefono);
            }
        }
    }
}
