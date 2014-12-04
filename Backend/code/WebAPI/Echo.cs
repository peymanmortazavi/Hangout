using System;
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Owin.Cors;

namespace Hangout.WebAPI
{

	public class Echo : PersistentConnection
	{

		protected override Task OnConnected (IRequest request, string connectionId)
		{
			Console.WriteLine ("{0} Connected.", connectionId);
			try
			{
				Console.WriteLine (request.User.Identity.Name);
			}
			catch(Exception exception)
			{
				Console.WriteLine ( "FAILED: " + exception.Message );
			}
			Connection.Broadcast ("Welcome !");
			return base.OnConnected (request, connectionId);
		}

		protected override Task OnDisconnected (IRequest request, string connectionId, bool stopCalled)
		{
			Console.WriteLine ("{0} Disconnected.", connectionId);
			return base.OnDisconnected (request, connectionId, stopCalled);
		}

		protected override Task OnReceived (IRequest request, string connectionId, string data)
		{
			Console.WriteLine ("[{0}] {1}", connectionId, data);
			return base.OnReceived (request, connectionId, data);
		}

	}
}