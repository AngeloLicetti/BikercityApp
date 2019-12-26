using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using System;
using Syncfusion.SfCalendar.XForms;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Ordenes
{
    public class HistorialCalendarViewModel : ViewModelBase
	{
		private IServicioService _servicioService;

		public HistorialCalendarViewModel(
								IServicioService servicioService
							 )
		{
			_servicioService = servicioService;
		}
		public override async Task InitializeAsync(object navigationData)
		{
            try
            {
                await base.InitializeAsync(navigationData);

                var listCitas = await _servicioService.GetCitaByUser();
                CalendarEvent = new CalendarEventCollection();
                foreach (var item in listCitas)
                {
                    CalendarInlineEvent event1 = new CalendarInlineEvent();
                    event1.StartTime = item.FechaInicio;
                    event1.EndTime = item.FechaFin;
                    event1.Subject = item.titulo;
                    var color = Color.FromHex("#28a745");
                    if (item.estadoCita == "POSPUESTO")
                        color = Color.FromHex("#ffc107");
                    if (item.estadoCita == "TERMINADO")
                        color = Color.FromHex("#007bff");
                    if (item.estadoCita == "CANCELADO")
                        color = Color.FromHex("#dc3545");
                    event1.Color = color;
                    CalendarEvent.Add(event1);
                }
            }
            catch (Exception ex)
            {
            }
			
			
			//List<OrdenModel> listOrden = await _ordenService.GetOrdenByCliente(Settings.idCliente);
			//OrdenList = listOrden.ToObservableCollection();
		}
		private CalendarEventCollection _calendarEvent;
		public CalendarEventCollection  CalendarEvent
		{
			get { return _calendarEvent; }
			set { _calendarEvent = value; RaisePropertyChanged(() => CalendarEvent); }
		}

	}
}
