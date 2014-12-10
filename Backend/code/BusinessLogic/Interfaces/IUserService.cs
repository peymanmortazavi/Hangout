
using Hangout.Entities;

namespace Hangout.BusinessLogic
{
	public interface IUserService
	{

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void CreateUser(User user);

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="user">The user.</param>
        void Login(string username, string password, out User user);

        /// <summary>
        /// Sends the friend request.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        FriendRequest SendFriendRequest(string userId);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        User GetUser(string id);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        User[] GetAllUsers();

        /// <summary>
        /// Gets all friend requests for current user.
        /// </summary>
        /// <returns></returns>
        FriendRequest[] GetAllFriendRequestsForCurrentUser();

        /// <summary>
        /// Accepts the friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request identifier.</param>
        void AcceptFriendRequest(string friendRequestId);

        /// <summary>
        /// Denies the friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request identifier.</param>
        void DenyFriendRequest(string friendRequestId);

	}
}