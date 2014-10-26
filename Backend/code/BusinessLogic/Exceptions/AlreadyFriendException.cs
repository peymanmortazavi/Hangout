
namespace Hangout.BusinessLogic
{
	public class AlreadyFriendException : BusinessException
	{
		public AlreadyFriendException (int errorCode, string message) : base(errorCode, message)
		{
		}
	}
}