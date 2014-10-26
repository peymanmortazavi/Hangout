using Hangout.BusinessLogic;

namespace Hangout.BusinessLogic
{
	public class NotFoundException : BusinessException
	{
		public NotFoundException (int errorCode, string message) : base(errorCode, message)
		{

		}

	}

}