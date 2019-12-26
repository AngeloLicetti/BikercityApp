using System.Collections.Generic;

namespace BikercityApp.Models.Common
{
	public class Constants
	{
		public const int ServiceTimeout = 7000;

		public struct ErrorTypes
		{
			public const string Service = "Service";
			public const string ViewModel = "ViewModel";
		}

		public struct HttpMethod
		{
			public const string Get = "Get";
			public const string Post = "Post";
			public const string Put = "Put";
		}
		public struct HttpHeaderConstants
		{
			public const string FilenameDefault = "nombre_no_disponible";
			public const string FilenameHeader = "x-filename";
		}
		public struct FormatosFechas
		{
			public const string formatoPE = "dd/MM/yyy";
		}
		public struct HttpCode
		{
			//OK 200
			public const int OkCode200 = 200; //Usuario sin enrolar
			public const int OkCode202 = 202; //Usuario enrolado

			//Error 400
			public const int ErrorCode409 = 409; //Conflict
		}

		public struct LoginMessage
		{
			//Error Login
			public const int ClienteInvalido = 1;
		}

		public struct ErrorMessage
		{
			//Error 400
			public const string ErrorMessage409 = "Conflict";
			public const string ErrorGeneral = "Ocurrió un error inesperado.";
		}

		public struct MensajesAlertas
		{
			public const string ArchivoDescargado = "El archivo se descargó en Documentos de su almacenamiento interno.";
			public const string ArchivoDescargadoTitulo = "Descarga Archivo";
			public const string ArchivoDescargadoBoton = "Aceptar";
		}
		public struct MensajeErrorService
		{
			public const string MensajeError = "Lo sentimos, la información solicitada por ahora no se encuentra disponible.";
			public const string TituloError = "Error";
			public const string MensajeErrorOpcion = "OK";
		}

		public struct CompletarDatosFiltro
		{
			public const string MensajeErrorFiltro = "Al menos debe seleccionar un Estado de la lista.";
			public const string TituloErrorFiltro = "Datos Incompletos";
			public const string MensajeErrorOpcion = "OK";
		}
		public struct TypeMensajeNav
		{
			public const string Cliente		= "Cliente";
			public const string Solicitud	= "Solicitud";
		}
		private static readonly Dictionary<int, string> _errorServiceDictionary = new Dictionary<int, string>()
		{
            //Error 400x
            {HttpCode.ErrorCode409, ErrorMessage.ErrorMessage409 },
		};

		public static Dictionary<int, string> ErrorServiceDictionary { get { return _errorServiceDictionary; } }

	}
}
