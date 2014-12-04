using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using System.Text;
using Hangout.Core;
using System.Threading;

namespace Hangout.WebAPI
{

	public class HangoutAuthorizeAttribute : AuthorizeAttribute
	{

		protected override bool IsAuthorized (HttpActionContext actionContext)
		{

			if (actionContext.Request.Headers.Authorization.Scheme.ToLower () != "id")
				return false;

			var encodedUserId = actionContext.Request.Headers.Authorization.Parameter;
			var id = Encoding.UTF8.GetString ( Convert.FromBase64String ( encodedUserId ) );
			var user = Services.UserService.GetUser ( id );

			string a = "";
			foreach (var item in ClaimsPrincipal.Current.FindAll (x => true)) {
				a += item.Type + " : " + item.Value + "\n";
			}

			Thread.CurrentPrincipal = new ClaimsPrincipal ();

			ClaimsPrincipal.Current.AddIdentity ( new ClaimsIdentity( new []{
				new Claim (HangoutClaims.Id, user.Id)
			} ) );

			return true;
		}

	}

}