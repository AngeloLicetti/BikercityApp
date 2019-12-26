using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Notification
{
    public interface INotificationService
    {
		Task<bool> registerToken(string fcmToken);

	}
}
