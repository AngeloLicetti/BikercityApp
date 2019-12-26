using BikercityApp.Services.Notification;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikercityApp.Helpers
{
    public static class NotificationHelper
    {
		public static async Task<bool> registerToken(string fcmToken)
		{
			return await ViewModelLocator.Resolve<INotificationService>().registerToken(fcmToken);
		}
    }
}
