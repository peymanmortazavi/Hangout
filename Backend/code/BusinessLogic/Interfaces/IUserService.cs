
using Hangout.Entities;

namespace Hangout.BusinessLogic
{
	public interface IUserService
	{

		void CreateUser(User user);

		void Login(string username, string password, out User user);

		FriendRequest SendFriendRequest(string userId);

		void AcceptFriendRequest(string friendRequestId);

		void DenyFriendRequest(string friendRequestId);

	}
}