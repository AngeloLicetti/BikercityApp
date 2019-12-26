using System;
using System.Runtime.Serialization;

namespace BikercityApp.Exceptions
{
	[System.SerializableAttribute]
	public class ServiceErrorCodeException : Exception
	{
		public int Code { get; }
		public string CodeMessage { get; }

		public ServiceErrorCodeException()
		{
		}

		public ServiceErrorCodeException(int code, string codeMessage)
		{
			Code = code;
			CodeMessage = codeMessage;
		}

		protected ServiceErrorCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
