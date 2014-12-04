using System.Web.Http;
using AutoMapper;
using Hangout.Entities;

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

			return Created (RequestContext.Url + "/" + user.Id, new { id = user.Id });

		}

		[HttpGet, Route("test")]
		public IHttpActionResult Test()
		{
			return Ok (new { firstName="Peyman", lastName="Mo" });
		}

	}

}