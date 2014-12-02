using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Owin.Security.OAuth;
using Hangout.Entities;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Threading.Tasks;
using System.Security.Claims;
using Hangout.Core;

namespace Hangout.WebAPI
{

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{

			app.UseCors(CorsOptions.AllowAll);

			InitializeAutoMapper ();

			// Rest API Setup
			var config = new HttpConfiguration(); 

			config.MapHttpAttributeRoutes ();

			// Use only JSON by default.
			var xmlFormatterSupportedMediaTypes = config.Formatters.XmlFormatter.SupportedMediaTypes;
			var appXmlType = xmlFormatterSupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			xmlFormatterSupportedMediaTypes.Remove(appXmlType);

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			config.Filters.Add(new ExceptionHandlerFilter(config.Formatters.JsonFormatter));

			app.UseWebApi(config);

			app.MapSignalR <Echo>("/test");

		}

		public static void InitializeAutoMapper()
		{

			Mapper.CreateMap<CreateUserModel, User> ();

			Mapper.CreateMap<User, UserInfoModel> ();

		}
	}
}