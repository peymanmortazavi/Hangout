using System;
using System.Linq;
using Hangout.Entities;
using System.Text.RegularExpressions;
using Hangout.DataAccess;
using Hangout.Core;
using System.Security.Claims;

namespace Hangout.BusinessLogic
{
	public class UserService : IUserService
	{

		IDataAccess _dataAccess;

		public UserService(IDataAccess dataAccess)
		{
			if (dataAccess == null)
				throw new ArgumentNullException ("dataAccess");

			this._dataAccess = dataAccess;
		}

		// TODO: throw correct exceptions.
		private void ValidateUser(User user)
		{
		
			if (string.IsNullOrWhiteSpace (user.FirstName))
				throw new Exception ("INVALID FIRST NAME");

			if (string.IsNullOrWhiteSpace (user.LastName))
				throw new Exception ("");

			if(Regex.IsMatch (user.PhoneNumber, @"^\d{3}-?\d{3}-?\d{4}$"))
				throw new Exception ("invalid phone number. pass 000-000-0000");

			if (Regex.IsMatch (user.Email, "^.*[@]{1}\\w*[.]{1}\\w{2,4}$"))
				throw new Exception ("invalid email");

			user.PhoneNumber = user.PhoneNumber.Replace ("-", string.Empty);

			if (Regex.IsMatch (user.Password, "^.{8,25}$"))
				throw new Exception ("");
				
		}

		public void CreateUser (User user)
		{

			if (user == null)
				throw new ArgumentNullException ("user");

			ValidateUser (user);

			if (_dataAccess.All<User> ().Any (x => x.Email == user.Email || x.PhoneNumber == user.PhoneNumber))
				throw new Exception ("");

			user.Password = PasswordHash.HashPassword (user.Password);

			_dataAccess.Add (user);

		}

		public void Login (string username, string password, out User user)
		{
		
			if (string.IsNullOrWhiteSpace (password))
				throw new ArgumentNullException ("password");

			if (string.IsNullOrWhiteSpace (username))
				throw new ArgumentNullException ("username");

			password = PasswordHash.HashPassword (password);

			user = _dataAccess.All<User> ().FirstOrDefault (x => x.Email == username && x.Password == password);

			if (user == null)
				throw new Exception ("not found yo!");

		}

		public FriendRequest SendFriendRequest (string userId)
		{

			// TODO: integrate with OWIN Claims Based Security and Identity Principal library
			var currentUserId = ClaimsPrincipal.Current.FindFirst (HangoutClaims.Id).Value;

			var targetUser = _dataAccess.Get<User> (userId);

			if (targetUser == null)
				throw new Exception ("user not found !");

			var currentUser = _dataAccess.Get<User> (currentUserId);

			if (currentUser.Friends.Contains (userId))
				throw new Exception ("you are already friends!!!");

			var friendRequest = new FriendRequest () {
				RequesterId = currentUserId,
				ReceiverId = userId
			};

			_dataAccess.Add (friendRequest);

			return friendRequest;

		}

		public void AcceptFriendRequest (string friendRequestId)
		{

			if (string.IsNullOrWhiteSpace (friendRequestId))
				throw new ArgumentException ("friendRequestId");

			var friendRequest = _dataAccess.Get<FriendRequest> (friendRequestId);

			if (friendRequest == null)
				throw new Exception ("not found !");

			var requesterUser = _dataAccess.Get<User> (friendRequest.RequesterId);

			if (requesterUser == null)
				throw new Exception ("they don't want to be your friends.");

			var receiverUser = _dataAccess.Get<User> (friendRequest.ReceiverId);

			if (receiverUser == null)
				throw new Exception ("oops they've run away cause you asked them to be friends !");

			requesterUser.Friends.Add (receiverUser.Id);

			receiverUser.Friends.Add (requesterUser.Id);

			_dataAccess.Delete<FriendRequest> (friendRequestId);

			_dataAccess.Update (requesterUser);

			_dataAccess.Update (receiverUser);

		}

		public void DenyFriendRequest (string friendRequestId)
		{

			if (string.IsNullOrWhiteSpace (friendRequestId))
				throw new ArgumentException ("friendRequestId");

			var friendRequest = _dataAccess.Get<FriendRequest> (friendRequestId);

			if (friendRequest == null)
				throw new Exception ("not found !");

			_dataAccess.Delete<FriendRequest> (friendRequestId);

		}

	}
}

