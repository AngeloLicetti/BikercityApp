using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikercityApp
{
	public class GlobalSettings : BindableObject
	{
		private const string defaultEndpointDev = "http://staminerdev-001-site5.ftempurl.com/";
		private const string defaultEndpointQa = "http://staminerdev-001-site5.ftempurl.com/";


        public GlobalSettings()
		{
			var result = Init().Result;
		}

		public async Task<bool> Init()
		{
			this.DefaultEndpoint = defaultEndpointDev;
			this.BaseEndpoint = DefaultEndpoint;
			this.Api_Key = "";
			return true;
		}



		public string DefaultEndpoint { get; set; }

		public string Api_Key { get; set; }

		private static readonly GlobalSettings _instance = new GlobalSettings();

		public int InactivityTimeOut
		{
			get;
			set;
		} = 0;

		public string SessionId { get; set; } = Guid.NewGuid().ToString();
		public DateTime LastActivity { get; set; } = DateTime.Now;

		public static GlobalSettings Instance
		{
			get { return _instance; }
		}

		public string BaseEndpoint
		{
			get { return _baseEndpoint; }
			set
			{
				_baseEndpoint = value;
				UpdateEndpoint(_baseEndpoint);
			}
		}

		public string ServiceEndpoint { get; set; }

		private bool _isHostReachable = true;
		private string _baseEndpoint;

		public bool IsHostReachable
		{
			get { return _isHostReachable; }
			set
			{
				_isHostReachable = value;
				OnPropertyChanged("IsHostReachable");
			}
		}

		private void UpdateEndpoint(string baseEndpoint)
		{
			ServiceEndpoint = string.Format("{0}", baseEndpoint);
		}

		public string UserId { get; internal set; }
	}
}
