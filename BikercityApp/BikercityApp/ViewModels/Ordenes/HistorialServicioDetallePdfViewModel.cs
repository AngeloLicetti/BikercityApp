using BikercityApp.Helpers;
using BikercityApp.Models.Common;
using BikercityApp.ViewModels.Base;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Ordenes
{
    public class HistorialServicioDetallePdfViewModel : ViewModelTab
	{
		#region Configuracion Inicial
		//  private byte[] bytePdf;
		private ManejoArchivoModel archivo;
		public HistorialServicioDetallePdfViewModel()
		{
		}
		public async override Task InitializeAsync(object navigationData)
		{
            try
            {
                await base.InitializeAsync(navigationData);
                archivo = navigationData as ManejoArchivoModel;
                PathFile = archivo.PdfPathFile;
            }
            catch (Exception ex)
            {
            }
			
		}
		#endregion

		#region Propiedades Bindeables
		private string _pathFile;
		public string PathFile
		{
			get { return _pathFile; }
			set
			{
				_pathFile = value;
				RaisePropertyChanged(() => PathFile);
			}
		}
		#endregion

		#region Comandos
		public ICommand CompartirCommand => new Command(async () => await Compartir());
		private async Task Compartir()
		{
			if (IsBusy)
			{
				return;
			}
			try
			{
				IsBusy = true;
				if (Xamarin.Forms.Device.RuntimePlatform == Device.iOS)
				{
					string path = DependencyService.Get<ISharePdf>().Save(new MemoryStream(Convert.FromBase64String(PathFile)), $"EECC_TC.pdf");
					await DependencyService.Get<ISharePdf>().Share("", "Compartiendo PDF", path);
				}
				else
				{
					await DependencyService.Get<ISharePdf>().Share("", "Sharing PDF", PathFile);
				}
			}
			finally
			{
				IsBusy = false;
			}
		}
		private string _textoInformacion;

		public string TextoInformacion
		{
			get { return _textoInformacion; }
			set { _textoInformacion = value; RaisePropertyChanged(() => TextoInformacion); }
		}

		public ICommand DescargarCommand => new Command(async () => await Descargar());
		private async Task Descargar()
		{
			IsBusy = true;

			IsModalVisible = true;
			TextoInformacion = "Su archivo se descargó exitosamente, verifique en su carpeta DOCUMENTOS";

			IsBusy = false;
		}
		#endregion
	}
}
