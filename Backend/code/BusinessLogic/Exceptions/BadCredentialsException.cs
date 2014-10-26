using Hangout.BusinessLogic;

namespace Hangout.BusinessLogic
{

	public class BadCredentialsException : BusinessException
	{

		public BadCredentialsException (int errorCode, string message) : base(errorCode, message)
		{
		}

	}

}