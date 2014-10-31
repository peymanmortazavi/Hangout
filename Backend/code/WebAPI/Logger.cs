using System;
using System.Collections.Generic;
using System.Text;

namespace Hangout.WebAPI
{
	public static class Logger
	{

		public static string Format = "[{0}] {1} {2} {3}";

		private static string SerializeData(Dictionary<string, string> data)
		{
			if (data == null)
				return string.Empty;

			var serializedData = new StringBuilder ();
			serializedData.Append ("\n");
			foreach (var element in data) 
			{
				serializedData.AppendFormat ("\t{0} : {1}\n", element.Key, element.Value);
			}
			return serializedData.ToString ();
		}

		public static void Trace(string msg)
		{
			Console.WriteLine (Format, "TRACE", DateTime.Now.ToString ("g"), msg, string.Empty);
		}

		public static void Trace(string msg, Dictionary<string, string> data)
		{
			Console.WriteLine (Format, "TRACE", DateTime.Now.ToString ("g"), msg, SerializeData (data));
		}

		public static void Debug(string msg)
		{
			Console.WriteLine (Format, "DEBUG", DateTime.Now.ToString ("g"), msg, string.Empty);
		}

		public static void Debug(string msg, Dictionary<string, string> data)
		{
			Console.WriteLine (Format, "DEBUG", DateTime.Now.ToString ("g"), msg, SerializeData (data));
		}

		public static void Info(string msg)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine (Format, "INFO", DateTime.Now.ToString ("g"), msg, string.Empty);
			Console.ForegroundColor = currentColor;
		}

		public static void Info(string msg, Dictionary<string, string> data)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine (Format, "INFO", DateTime.Now.ToString ("g"), msg, SerializeData (data));
			Console.ForegroundColor = currentColor;
		}

		public static void Warning(string msg)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine (Format, "WARN", DateTime.Now.ToString ("g"), msg, string.Empty);
			Console.ForegroundColor = currentColor;
		}

		public static void Warning(string msg, Dictionary<string, string> data)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine (Format, "WARN", DateTime.Now.ToString ("g"), msg, SerializeData (data));
			Console.ForegroundColor = currentColor;
		}

		public static void Error(string msg)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (Format, "ERROR", DateTime.Now.ToString ("g"), msg, string.Empty);
			Console.ForegroundColor = currentColor;
		}

		public static void Error(string msg, Dictionary<string, string> data)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (Format, "ERROR", DateTime.Now.ToString ("g"), msg, SerializeData (data));
			Console.ForegroundColor = currentColor;
		}

		public static void Error(string msg, Exception exception, Dictionary<string, string> data=null)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (Format, "ERROR", DateTime.Now.ToString ("g"), msg, SerializeData (data)+"\n\tException Message: " + exception.Message);
			Console.ForegroundColor = currentColor;
		}
	}
}