using System;
using System.Linq;
using Hangout.Entities;
using System.Text.RegularExpressions;
using Hangout.DataAccess;
using Hangout.Core;
using System.Security.Claims;
using System.Collections.Generic;

namespace Hangout.BusinessLogic
{
	public class UserService : IUserService
	{

		readonly IDataAccess _dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        /// <exception cref="System.ArgumentNullException">dataAccess</exception>
        public UserService(IDataAccess dataAccess)
		{
			if (dataAccess == null)
				throw new ArgumentNullException ("dataAccess");

			_dataAccess = dataAccess;
		}

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="ValidationException">User is not valid.</exception>
        private static void ValidateUser(User user)
		{

			var details = new Dictionary<string, string> ();
		
			if (string.IsNullOrWhiteSpace (user.FirstName))
				details ["FirstName"] = "First name cannot be empty.";

			if (string.IsNullOrWhiteSpace (user.LastName))
				details ["LastName"] = "Last name cannot be empty.";

			if (string.IsNullOrWhiteSpace (user.PhoneNumber) || !Regex.IsMatch (user.PhoneNumber, @"^\d{3}-?\d{3}-?\d{4}$"))
				details ["PhoneNumber"] = "Enter a valid U.S. phone number. 000-000-0000";
			else
				user.PhoneNumber = user.PhoneNumber.Replace ("-", string.Empty);

			if (string.IsNullOrWhiteSpace (user.Email) || !Regex.IsMatch (user.Email, "^.*[@]{1}\\w*[.]{1}\\w{2,4}$"))
				details ["Email"] = "Please enter a valid email.";

			if (string.IsNullOrWhiteSpace (user.Password) || !Regex.IsMatch (user.Password, "^.{8,25}$"))
				details["Password"] = "Pass must be at least 8 characters and less than 25 characters.";
				
			if (details.Count > 0)
				throw new ValidationException (ErrorCodes.EntityValidationError, details, "User is not valid.");
		}

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">user</exception>
        /// <exception cref="Hangout.BusinessLogic.DuplicateEntityException">
        /// PhoneNumber
        /// or
        /// Email
        /// </exception>
        public void CreateUser (User user)
		{

			if (user == null)
				throw new ArgumentNullException ("user");

			ValidateUser (user);

			var existingUser = _dataAccess.All<User> ().FirstOrDefault (x => x.Email == user.Email || x.PhoneNumber == user.PhoneNumber);
			if (existingUser != null) 
			{
				if(existingUser.PhoneNumber == user.PhoneNumber)
					throw new DuplicateEntityException (ErrorCodes.EntityDuplicatedError, "PhoneNumber", string.Format ("Phone number '{0}' is taken.", user.PhoneNumber));

				if(existingUser.Email == user.Email)
					throw new DuplicateEntityException (ErrorCodes.EntityDuplicatedError, "Email", string.Format ("Email '{0}' is taken.", user.Email));
			}

			user.Password = PasswordHash.HashPassword (user.Password);

			_dataAccess.Add (user);

		}

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">
        /// password
        /// or
        /// username
        /// </exception>
        /// <exception cref="Hangout.BusinessLogic.BadCredentialsException">Wrong email and password!</exception>
        public void Login (string username, string password, out User user)
		{
		
			if (string.IsNullOrWhiteSpace (password))
				throw new ArgumentNullException ("password");

			if (string.IsNullOrWhiteSpace (username))
				throw new ArgumentNullException ("username");

			password = PasswordHash.HashPassword (password);

			user = _dataAccess.All<User> ().FirstOrDefault (x => x.Email == username && x.Password == password);

			if (user == null)
				throw new BadCredentialsException (ErrorCodes.BadCredentialsError, "Wrong email and password!");

		}

        /// <summary>
        /// Sends the friend request.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Hangout.BusinessLogic.NotFoundException">User not found.</exception>
        /// <exception cref="Hangout.BusinessLogic.AlreadyFriendException">You are already </exception>
        public FriendRequest SendFriendRequest (string userId)
		{

			// TODO: integrate with OWIN Claims Based Security and Identity Principal library
			var currentUserId = ClaimsPrincipal.Current.FindFirst (HangoutClaims.Id).Value;

			var targetUser = _dataAccess.Get<User> (userId);

			if (targetUser == null)
				throw new NotFoundException (ErrorCodes.EntityNotFoundError, "User not found.");

			var currentUser = _dataAccess.Get<User> (currentUserId);

			if (currentUser.Friends.Contains (userId))
				throw new AlreadyFriendException (ErrorCodes.AlreadyFriendError, "You are already ");

			var friendRequest = new FriendRequest {
				RequesterId = currentUserId,
				ReceiverId = userId
			};

			_dataAccess.Add (friendRequest);

			return friendRequest;

		}

        /// <summary>
        /// Accepts the friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request identifier.</param>
        /// <exception cref="System.ArgumentException">friendRequestId</exception>
        /// <exception cref="Hangout.BusinessLogic.NotFoundException">
        /// Friend request not found!
        /// or
        /// User not found!
        /// or
        /// User not found.
        /// </exception>
        public void AcceptFriendRequest (string friendRequestId)
		{

			if (string.IsNullOrWhiteSpace (friendRequestId))
				throw new ArgumentException ("friendRequestId");

			var friendRequest = _dataAccess.Get<FriendRequest> (friendRequestId);

			if (friendRequest == null)
				throw new NotFoundException (ErrorCodes.EntityNotFoundError, "Friend request not found!");

			var requesterUser = _dataAccess.Get<User> (friendRequest.RequesterId);

			if (requesterUser == null)
				throw new NotFoundException (ErrorCodes.EntityNotFoundError, "User not found!");

			var receiverUser = _dataAccess.Get<User> (friendRequest.ReceiverId);

			if (receiverUser == null)
				throw new NotFoundException (ErrorCodes.EntityNotFoundError, "User not found.");

			requesterUser.Friends.Add (receiverUser.Id);

			receiverUser.Friends.Add (requesterUser.Id);

			_dataAccess.Delete<FriendRequest> (friendRequestId);

			_dataAccess.Update (requesterUser);

			_dataAccess.Update (receiverUser);

		}

        /// <summary>
        /// Denies the friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request identifier.</param>
        /// <exception cref="System.ArgumentException">friendRequestId</exception>
        /// <exception cref="Hangout.BusinessLogic.NotFoundException">Friend request not found!</exception>
        public void DenyFriendRequest (string friendRequestId)
		{

			if (string.IsNullOrWhiteSpace (friendRequestId))
				throw new ArgumentException ("friendRequestId");

			var friendRequest = _dataAccess.Get<FriendRequest> (friendRequestId);

			if (friendRequest == null)
				throw new NotFoundException (ErrorCodes.EntityNotFoundError, "Friend request not found!");

			_dataAccess.Delete<FriendRequest> (friendRequestId);

		}

	}
}