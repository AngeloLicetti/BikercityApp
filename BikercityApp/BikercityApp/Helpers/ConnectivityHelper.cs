using Plugin.Connectivity;
using System;
using System.Threading.Tasks;

namespace BikercityApp.Helpers
{
	public static class ConnectivityHelper
	{
		private static DateTime _lastCheckDateConnectivity;
		private static bool _lastCheckResponseConnectivity;

		static ConnectivityHelper()
		{
			_lastCheckDateConnectivity = DateTime.MinValue;
			_lastCheckResponseConnectivity = false;
		}

		public static async Task<bool> CheckConnectivityAsync()
		{
			bool response;

			if (CrossConnectivity.Current.IsConnected)
			{
				if (_lastCheckDateConnectivity.AddSeconds(3) < DateTime.Now)
				{
					_lastCheckDateConnectivity = DateTime.Now;
					string host = "https://www.google.com.pe/";

					bool isHostReachable;
					isHostReachable = await CrossConnectivity.Current.IsRemoteReachable(host);

					GlobalSettings.Instance.IsHostReachable = isHostReachable;
					response = isHostReachable;
				}
				else
				{
					response = _lastCheckResponseConnectivity;
				}
			}
			else
			{
				GlobalSettings.Instance.IsHostReachable = false;
				response = false;
			}

			_lastCheckResponseConnectivity = response;

			return response;
		}
	}
}
