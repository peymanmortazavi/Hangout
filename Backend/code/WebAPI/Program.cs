using System;
using Microsoft.Owin.Hosting;
using Hangout.BusinessLogic;
using Hangout.DataAccess;

namespace Hangout.WebAPI
{
	public class MainClass
	{

		public static void Main (string[] args)
		{

			var dataAccess = new MongoDataAccess ("mongodb://localhost/Hangout");
			Services.UserService = new UserService (dataAccess);

			const string url = "http://*:8080";
			using (WebApp.Start<Startup>(url))
			{
				Logger.Info ("Server running on " + url);
				Console.ReadLine();
			}

		}
	}

	public static class Services
	{

		public static IUserService UserService { get; set; }

	}

}