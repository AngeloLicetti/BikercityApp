using System;
using System.Runtime.Serialization;

namespace BikercityApp.Exceptions
{
	[System.SerializableAttribute]
	public class ServiceAuthenticationException : Exception
	{
		public string Content { get; }

		public ServiceAuthenticationException()
		{
		}

		public ServiceAuthenticationException(string content)
		{
			Content = content;
		}

		protected ServiceAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
