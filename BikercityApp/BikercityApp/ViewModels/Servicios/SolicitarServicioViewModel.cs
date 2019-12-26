using BikercityApp.Helpers;
using BikercityApp.Models.Common;
using BikercityApp.Models.Servicios;
using BikercityApp.Models.Servicios.ModeloServicio;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.Catalogo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Servicios
{
    public class SolicitarServicioViewModel : ViewModelBase
    {
        private IServicioService _servicioService;

        public SolicitarServicioViewModel(
                                IServicioService servicioService
                             )
        {
            _servicioService = servicioService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
			try
			{
				DetalleServicioModel model = navigationData as DetalleServicioModel;
				DetalleServicio = model;
			}
			catch (Exception ex)
			{

			}
        }

        private bool _displayPopup;
        public bool DisplayPopup
        {
            get { return _displayPopup; }
            set { _displayPopup = value; RaisePropertyChanged(() => DisplayPopup); }
        }
        private bool _isSolicitar = true;
        public bool IsSolicitar
        {
            get { return _isSolicitar; }
            set { _isSolicitar = value; RaisePropertyChanged(() => IsSolicitar); }
        }
        private bool _displayPopup2;
        public bool DisplayPopup2
        {
            get { return _displayPopup2; }
            set { _displayPopup2 = value; RaisePropertyChanged(() => DisplayPopup2); }
        }

        private string _nombreContacto;
        public string NombreContacto
        {
            get { return _nombreContacto; }
            set { _nombreContacto = value; RaisePropertyChanged(() => NombreContacto); }
        }

        private string _correo;
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; RaisePropertyChanged(() => Correo); }
        }

        private string _direccion;
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; RaisePropertyChanged(() => Direccion); }
        }

        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; RaisePropertyChanged(() => Telefono); }
        }

        private string _comentario;
        public string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; RaisePropertyChanged(() => Comentario); }
        }

        private DetalleServicioModel _detalleServicio;
        public DetalleServicioModel DetalleServicio
        {
            get { return _detalleServicio; }
            set { _detalleServicio = value; RaisePropertyChanged(() => DetalleServicio); }
        }

		private string _textoInformacion;

		public string TextoInformacion
		{
			get { return _textoInformacion; }
			set { _textoInformacion = value; RaisePropertyChanged(() => TextoInformacion); }
		}



		public ICommand SolicitarServicioCommand => new Command(async () => await SolicitarServicio());
        private async Task SolicitarServicio()
        {
            DisplayPopup2 = false ;
            try
            {
                SolicitudServicioInput servicioInput = new SolicitudServicioInput();
                servicioInput.idCliente = Settings.idCliente;
                servicioInput.correoContacto = Correo;
                servicioInput.comentario = Comentario;
                servicioInput.idServicio = DetalleServicio.idServicio;
                servicioInput.idEstadoServicio = 1;
                servicioInput.nombreContacto = NombreContacto;
                servicioInput.direccionContacto = Direccion;
                servicioInput.telefono = Telefono;

                var response = await _servicioService.SolicitarServicio(servicioInput);
                if (response.result)
                {
					IsModalVisible = true;
					TextoInformacion = "Se guardó exitosamente su solicitud servicio con número: "+response.Id;
                }
                else
                {
					IsModalVisible = true;
					TextoInformacion = response.message;
				}
            }
            catch (Exception ex)
            {
				IsModalVisible = true;
				TextoInformacion = Constants.ErrorMessage.ErrorGeneral;
            }

        }
        public ICommand GoToCatalogoViewCommand => new Command(async () => await GoToCatalogoView());

        private async Task GoToCatalogoView()
        {
            await NavigationService.NavigateToAsync<CatalogoViewModel>();
        }

        public ICommand ChangeDisplayCommand => new Command(async () => await ChangeDisplay());

        private async Task ChangeDisplay()
        {
            DisplayPopup2 = false;
        }

    }
}
