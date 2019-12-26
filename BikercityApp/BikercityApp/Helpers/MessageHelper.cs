using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikercityApp.Helpers
{
	public class MessageHelper
	{
		public static MessageHelper instance;

		private static MessageHelper GetInstance()
		{
			if (instance == null)
			{
				instance = new MessageHelper();
			}
			return instance;
		}
		public static void PredeterminarTarjetaResumen()
		{
			MessagingCenter.Send(GetInstance(), MessageKeys.TarjetaPredeterminadaKey, "");
		}
		public static void PredeterminarDireccionResumen()
		{
			MessagingCenter.Send(GetInstance(), MessageKeys.DireccionPredeterminadaKey, "");
		}
		public static void ListarDirecciones()
		{
			MessagingCenter.Send(GetInstance(), MessageKeys.ListarDireccionesKey, "");
		}
	}
}

