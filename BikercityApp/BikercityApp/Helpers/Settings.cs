using BikercityApp.Models.Catalogo;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikercityApp.Helpers
{
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
        private const string clienteIDKey = "clienteID_key";
        private const string nombreClienteKey = "nombreCliente_key";
        private const string paternoClienteKey = "paternoCliente_key";
        private const string maternoClienteKey = "maternoCliente_key";

		private const string idClienteKey = "idCliente_key";
		private const string loggedInKey = "loggedIn_key";
		private const string FCMTokenKey = "FCMToken_key";


		private const string ShoppingCartKey = "ShoppingCart_key";

		private static readonly string SettingsDefault = string.Empty;
		private static readonly int SettingsIntDefault = 0;
		private static readonly int intSettingsDefault = 0;
		private static readonly string stringSettingsDefault = string.Empty;
		private static readonly bool boolSettingsDefault = false;

		private const string ListSuppliesKey = "ListSuppliesKey";
		public static string ShoppingCartDefault = JsonConvert.SerializeObject(new List<DetalleProductoModel>());

		#endregion
		
		public static List<DetalleProductoModel> ShoppingCartList
		{
			get
			{
				string value = AppSettings.GetValueOrDefault(ShoppingCartKey, ShoppingCartDefault);
				return JsonConvert.DeserializeObject<List<DetalleProductoModel>>(value);
			}
			set
			{
				AppSettings.AddOrUpdateValue(ShoppingCartKey, JsonConvert.SerializeObject(value));
			}
		}

		public static bool loggedIn
		{
			get
			{
				return AppSettings.GetValueOrDefault(loggedInKey, boolSettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(loggedInKey, value);
			}
		}
		public static string FCMToken
		{
			get
			{
				return AppSettings.GetValueOrDefault(FCMTokenKey, stringSettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(FCMTokenKey, value);
			}
		}

		public static int idCliente
		{
			get
			{
				return AppSettings.GetValueOrDefault(idClienteKey, intSettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(idClienteKey, value);
			}
		}

        public static string nombreCliente
        {
            get
            {
                return AppSettings.GetValueOrDefault(nombreClienteKey, stringSettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nombreClienteKey, value);
            }
        }
        public static string paternoCliente
        {
            get
            {
                return AppSettings.GetValueOrDefault(paternoClienteKey, stringSettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(paternoClienteKey, value);
            }
        }
        public static string maternoCliente
        {
            get
            {
                return AppSettings.GetValueOrDefault(maternoClienteKey, stringSettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(maternoClienteKey, value);
            }
        }


        public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}
		

		//public static List<SuppliesItemModel> ListSupplies
		//{
		//	get
		//	{
		//		string value = AppSettings.GetValueOrDefault(ListSuppliesKey, SuppliesDefault);
		//		return JsonConvert.DeserializeObject<List<SuppliesItemModel>>(value);
		//	}
		//	set
		//	{
		//		AppSettings.AddOrUpdateValue(ListSuppliesKey, JsonConvert.SerializeObject(value));
		//	}
		//}

	}
}
