using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.User;
using BikercityApp.Extensions;
using BikercityApp.Services.Direccion;
using BikercityApp.Services.Cliente;
using BikercityApp.Services.Ordenes;
using BikercityApp.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BikercityApp.ViewModels.Catalogo;
using BikercityApp.Helpers;
using BikercityApp.Models.Catalogo;
using BikercityApp.Services.Tarjeta;
using BikercityApp.Models.Catalogo.ModeloServicio;

namespace BikercityApp.ViewModels.User
{
    public class AgregarTarjetaViewModel : ViewModelBase
    {
        private ITarjetaService _tarjetaService;
        public AgregarTarjetaViewModel(ITarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
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
        public ICommand AgregarTarjetaCommand => new Command(async () => await AgregarTarjeta());
        private async Task AgregarTarjeta()
        {
            IsUpdating = true;
            try
            {
                TarjetaModel tarjetaModel = new TarjetaModel();
                tarjetaModel.NumeroTarjeta = NumeroTarjeta;
                tarjetaModel.mes = Mes;
                tarjetaModel.año = Año;
                tarjetaModel.cvv = Cvv;
                tarjetaModel.nombre = Nombre;
                tarjetaModel.apellidos = Apellidos;
                tarjetaModel.idCliente = Settings.idCliente;
                var response = await _tarjetaService.agregarTarjeta(tarjetaModel);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsUpdating = false;
            }
            IsUpdating = false;
            await NavigationService.NavigateToAsync<ConfirmarPedidoViewModel>();
        }

        private string _numerotarjeta;

        public string NumeroTarjeta
        {
            get { return _numerotarjeta; }
            set
            {
                _numerotarjeta = value;
                RaisePropertyChanged(() => NumeroTarjeta);
            }
        }

        private int _mes;

        public int Mes
        {
            get { return _mes; }
            set
            {
                _mes = value;
                RaisePropertyChanged(() => Mes);
            }
        }

        private int _año;

        public int Año
        {
            get
            {
                return _año;
            }
            set
            {
                _año = value;
                RaisePropertyChanged(() => Año);
            }
        }

        private int _cvv;

        public int Cvv
        {
            get { return _cvv; }
            set
            {
                _cvv = value;
                RaisePropertyChanged(() => Cvv);
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

        private string _apellidos;

        public string Apellidos
        {
            get { return _apellidos; }
            set
            {
                _apellidos = value;
                RaisePropertyChanged(() => Apellidos);
            }
        }
    }
}
