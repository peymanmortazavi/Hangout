using System;
using System.Dynamic;

namespace Hangout.BusinessLogic
{
	public class BusinessException : Exception
	{

		public BusinessException(int errorCode, string message) : base(message)
		{
			ErrorCode = errorCode;
		}

		public BusinessException(int errorCode)
			: this(errorCode, "")
		{

		}

		public int ErrorCode { get; set; }

		public virtual object ToSerializableObject()
		{
			dynamic @object = new ExpandoObject();
			@object.code = ErrorCode;
			@object.msg = Message;
			return @object;
		}

	}
}