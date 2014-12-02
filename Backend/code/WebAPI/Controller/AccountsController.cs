using System.Web.Http;
using AutoMapper;
using Hangout.Entities;
using Hangout.Core;
using System.Security.Claims;
using System.Linq;
using System;
using System.Text;

namespace Hangout.WebAPI
{
	[RoutePrefix("accounts")]
	public class AccountsController : ApiController
	{

		[HttpPost, Route("")]
		public IHttpActionResult CreateUser(CreateUserModel model)
		{

			var user = Mapper.Map<User> (model);

			Services.UserService.CreateUser (user);

			return Created (RequestContext.Url.Request.RequestUri + "/" + user.Id, new { id = user.Id });

		}

		[HttpPost, Route("authenticate")]
		public IHttpActionResult Login(LoginModel model)
		{

			User user;

			Services.UserService.Login (model.Email, model.Password, out user);

			var encodedId = Convert.ToBase64String ( Encoding.UTF8.GetBytes ( user.Id ) );

			return Ok( new { token=encodedId, firstName=user.FirstName, lastName=user.LastName } );

		}
			
		[HttpGet, Route("")]
		[HangoutAuthorize]
		public IHttpActionResult GetUserInfo()
		{

			var user = Services.UserService.GetUser ( ClaimsPrincipal.Current.FindFirst (HangoutClaims.Id).Value );
			var model = Mapper.Map<User, UserInfoModel> (user);
			return Ok (model);
		}

		[HttpPost, Route("lookup")]
		public IHttpActionResult LookupUsers(string[] phones)
		{

			var users = Services.UserService.GetAllUsers ()
				.Where (x => phones.Contains (x.PhoneNumber) )
				.Select (x => Mapper.Map<User,UserInfoModel> (x));

			return Ok (users);

		}

		[HttpPost, Route("friendrequests")]
		[HangoutAuthorize]
		public IHttpActionResult CreateFriendRequest(string id)
		{

			Services.UserService.SendFriendRequest ( id );

			return Ok ();

		}

		[HttpGet, Route("friendrequests")]
		[HangoutAuthorize]
		public IHttpActionResult GetFriendRequests(  )
		{

			return Ok(Services.UserService.GetAllFriendRequestsForCurrentUser ());

		}

		[HttpPut, Route("friendrequests/{friendRequestId}")]
		[HangoutAuthorize]
		public IHttpActionResult UpdateFriendRequest( string friendRequestId, bool accept )
		{

			if(accept)
				Services.UserService.AcceptFriendRequest (friendRequestId);
			else
				Services.UserService.DenyFriendRequest (friendRequestId);

			return Ok();

		}

		[HttpGet, Route("friends")]
		[HangoutAuthorize]
		public IHttpActionResult GetFriends()
		{

			var user = Services.UserService.GetUser ( ClaimsPrincipal.Current.FindFirst (HangoutClaims.Id).Value );
			var models = user.Friends
				.Select ( x => Mapper.Map<User,UserInfoModel> (Services.UserService.GetUser ( x )) );

			return Ok (models);

		}

	}

}