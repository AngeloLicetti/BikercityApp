using BikercityApp.Extensions;
using BikercityApp.Helpers;
using BikercityApp.Models.Common;
using BikercityApp.Models.Servicios;
using BikercityApp.Models.Servicios.ModeloServicio;
using BikercityApp.Models.Servicios.Navigation;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Ordenes
{
    public class HistorialServicioDetalleViewModel : ViewModelBase
    {
        private IServicioService _ServicioService;
        private ServicioModel servicioModel;
        public HistorialServicioDetalleViewModel(
                                IServicioService ServicioService
                             )
        {
            _ServicioService = ServicioService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            try
            {
                servicioModel = navigationData as ServicioModel;
                await updateList();
            }
            catch (Exception e)
            {
                return;
            }
            IsBusy = false;
        }
        private async Task updateList()
        {
            try
            {
                Servicio = await _ServicioService.GetDetalleServicioByID(servicioModel.idSolicitudServicio);
                DetalleServicioList = Servicio.citas.ToObservableCollection();
                if (DetalleServicioList.Any(c => c.EstadoCita != "TERMINADO"))
                    IsAgendable = false;
                else
                    IsAgendable = true;
                DateSelected = DateTime.Now.AddDays(1);
            }
            catch (Exception ex)
            {
            }
            
        }
        private bool _isAgendable = false;

        public bool IsAgendable
        {
            get { return _isAgendable; }
            set { _isAgendable = value; RaisePropertyChanged(() => IsAgendable); }
        }
        private ObservableCollection<CitaServicioModel> _detalleServicioList;

        public ObservableCollection<CitaServicioModel> DetalleServicioList
        {
            get { return _detalleServicioList; }
            set { _detalleServicioList = value; RaisePropertyChanged(() => DetalleServicioList); }
        }
        private DetalleServicioModel _servicio;

        public DetalleServicioModel Servicio
        {
            get { return _servicio; }
            set { _servicio = value; RaisePropertyChanged(() => Servicio); }
        }
        private byte[] bytePdf;
        public ICommand VerPdfCommand => new Command(async (item) => await VerPdf());
        private async Task VerPdf()
        {
            IsBusy = true;
            bool granted = true;
            try
            {
                Permission[] permissions = null;
                if (Device.RuntimePlatform == Device.Android)
                {
                    permissions = new Permission[] { Permission.Storage };
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(permissions);
                    foreach (var item in results)
                    {
                        if (item.Value != PermissionStatus.Granted && (item.Value != PermissionStatus.Unknown))
                        {
                            granted = false;
                            //var result = await DialogService.ShowConfirmAlertAsync("No se puede continuar hasta que acepte los permisos.", "Permisos Rechazados", "Ir a configuracion", "Cancelar");
                            //if (result)
                            //{
                            var response = Plugin.Permissions.CrossPermissions.Current.OpenAppSettings();
                            if (!response)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Device.OpenUri(new Uri("app-settings:"));
                                });
                            }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            if (granted)
            {
                if (Servicio.urlpdf != null)
                {
                    ManejoArchivoModel archivoModel = new ManejoArchivoModel();
                    bytePdf = (await _ServicioService.GetBytesPdf(Servicio.urlpdf));
                    archivoModel.Nombre = Servicio.nombrePdf;
                    await IrAlPdf(archivoModel);
                }
            }
            IsBusy = false;

        }

        public async Task IrAlPdf(ManejoArchivoModel archivoModel)
        {
            if (bytePdf != null)
            {

                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                {
                    string PathFile = Convert.ToBase64String(bytePdf);
                    archivoModel.PdfPathFile = PathFile;
                    archivoModel.PdfBytes = bytePdf;
                    await NavigationService.NavigateToAsync<HistorialServicioDetallePdfViewModel>(archivoModel);
                }
                else
                {
                    string PathFile = await DependencyService.Get<IFileHelper>().WritePdfFile(bytePdf, archivoModel.Nombre);
                    archivoModel.PdfPathFile = PathFile;
                    archivoModel.PdfBytes = bytePdf;
                    await NavigationService.NavigateToAsync<HistorialServicioDetallePdfViewModel>(archivoModel);
                }
            }
        }
        private bool _isModalCita = false;

        public bool IsModalCita
        {
            get { return _isModalCita; }
            set { _isModalCita = value; RaisePropertyChanged(() => IsModalCita); }
        }
        private ObservableCollection<HorarioModel> _horarioList;

        public ObservableCollection<HorarioModel> HorarioList
        {
            get { return _horarioList; }
            set { _horarioList = value; RaisePropertyChanged(() => HorarioList); }
        }
        private HorarioModel _horarioSelected;

        public HorarioModel HorarioSelected
        {
            get { return _horarioSelected; }
            set
            {
                if (value != null)
                {
                    _horarioSelected = value;
                    RaisePropertyChanged(() => HorarioSelected);
                }
            }
        }
        private DateTime _minDate = DateTime.Today.AddDays(1);

        public DateTime MinDate
        {
            get { return _minDate; }
            set { _minDate = value; RaisePropertyChanged(() => MinDate); }
        }
        private DateTime _maxDate = DateTime.Today.AddDays(8);

        public DateTime MaxDate
        {
            get { return _maxDate; }
            set { _maxDate = value; RaisePropertyChanged(() => MaxDate); }
        }
        private DateTime _dateSelected = DateTime.Today.AddDays(1);

        public DateTime DateSelected
        {
            get { return _dateSelected; }
            set
            {
                _dateSelected = value;
                Task.Run(async () => await getHorarios(_dateSelected));
                RaisePropertyChanged(() => DateSelected);
            }
        }
        private async Task getHorarios(DateTime dateSelected)
        {
            IsBusy = true;
            try
            {
                var horarios = await _ServicioService.GetHorariosDisponibles(dateSelected);
                HorarioList = horarios.ToObservableCollection();
                HorarioSelected = HorarioList.FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            IsBusy = false;
        }

        public ICommand OpenCitaModalCommand => new Command(async () => await openCitaModal());
        private async Task openCitaModal()
        {
            DateSelected = DateTime.Now.AddDays(1);
            citaSeleccionada = null;
            IsModalCita = true;
        }

        public ICommand CloseCitaModalCommand => new Command(async () => await CloseCitaModal());
        private async Task CloseCitaModal()
        {
            IsModalCita = false;
        }
        public ICommand AgendarCitaCommand => new Command(async () => await AgendarCita());
        private async Task AgendarCita()
        {
            IsBusy = true;
            try
            {
                BaseResponse response;
                if (citaSeleccionada == null)
                {
                    CitaInputModel citaInput = new CitaInputModel
                    {
                        Fecha = DateSelected,
                        IdHoras = HorarioSelected.IdHorario,
                        IdEstadoCita = 1,
                        IdSolicitudServicio = Servicio.idSolicitudServicio
                    };
                    response = await _ServicioService.CreateCita(citaInput);
                }
                else
                {
                    if (DateSelected == citaSeleccionada.Fecha && HorarioSelected.IdHorario == citaSeleccionada.idHoras)
                    {
                        //error
                        response = new BaseResponse();
                    }
                    else
                    {
                        CitaUpdateInputModel citaInput = new CitaUpdateInputModel
                        {
                            Fecha = DateSelected,
                            IdHoras = HorarioSelected.IdHorario,
                            IdCita = citaSeleccionada.idCita
                        };
                        response = await _ServicioService.PosponerCita(citaInput);
                    }
                }

                if (response.result)
                {
                    await updateList();
                }
                else
                {
                    //error
                }
                IsModalCita = false;
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }
        private CitaServicioModel citaSeleccionada;
        public ICommand OpenpUpdateModalCommand => new Command<CitaServicioModel>(async (item) => await OpenpUpdateModal(item));
        private async Task OpenpUpdateModal(CitaServicioModel cita)
        {
            IsBusy = true;
            DateSelected = cita.Fecha;
            citaSeleccionada = cita;
            IsModalCita = true;
            IsBusy = false;
        }
        public ICommand CancelarCitaCommand => new Command<CitaServicioModel>(async (item) => await CancelarCita(item));
        private async Task CancelarCita(CitaServicioModel cita)
        {
            IsBusy = true;
            try
            {
                CitaCancelInputModel citaInput = new CitaCancelInputModel
                {
                    IdCita = cita.idCita
                };
                var response = await _ServicioService.CancelarCita(citaInput);
                if (response.result)
                {
                    await updateList();
                }
                else
                {
                    //error
                }
                IsModalCita = false;
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }

		public ICommand GoToMensajeCommand => new Command(async () => await GoToMensaje());
		private async Task GoToMensaje()
		{
			await NavigationService.NavigateToAsync<HistorialMensajesViewModel>(new MensajeNavModel
			{
				id = Servicio.idSolicitudServicio,
				type = Constants.TypeMensajeNav.Solicitud
			});
		}
	}
}
