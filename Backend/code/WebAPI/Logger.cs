using System;
using System.Collections.Generic;
using System.Text;

namespace Hangout.WebAPI
{
	public static class Logger
	{

		public static string Format = "[{0}] {1} {2} {3}";

        /// <summary>
        /// Serializes the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Traces the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public static void Trace(string msg)
		{
			Console.WriteLine (Format, "TRACE", DateTime.Now.ToString ("g"), msg, string.Empty);
		}

        /// <summary>
        /// Traces the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="data">The data.</param>
        public static void Trace(string msg, Dictionary<string, string> data)
		{
			Console.WriteLine (Format, "TRACE", DateTime.Now.ToString ("g"), msg, SerializeData (data));
		}

        /// <summary>
        /// Debugs the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public static void Debug(string msg)
		{
			Console.WriteLine (Format, "DEBUG", DateTime.Now.ToString ("g"), msg, string.Empty);
		}

        /// <summary>
        /// Debugs the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="data">The data.</param>
        public static void Debug(string msg, Dictionary<string, string> data)
		{
			Console.WriteLine (Format, "DEBUG", DateTime.Now.ToString ("g"), msg, SerializeData (data));
		}

        /// <summary>
        /// Informations the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public static void Info(string msg)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine (Format, "INFO", DateTime.Now.ToString ("g"), msg, string.Empty);
			Console.ForegroundColor = currentColor;
		}

        /// <summary>
        /// Informations the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="data">The data.</param>
        public static void Info(string msg, Dictionary<string, string> data)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine (Format, "INFO", DateTime.Now.ToString ("g"), msg, SerializeData (data));
			Console.ForegroundColor = currentColor;
		}

        /// <summary>
        /// Warnings the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public static void Warning(string msg)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine (Format, "WARN", DateTime.Now.ToString ("g"), msg, string.Empty);
			Console.ForegroundColor = currentColor;
		}

        /// <summary>
        /// Warnings the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="data">The data.</param>
        public static void Warning(string msg, Dictionary<string, string> data)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine (Format, "WARN", DateTime.Now.ToString ("g"), msg, SerializeData (data));
			Console.ForegroundColor = currentColor;
		}

        /// <summary>
        /// Errors the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public static void Error(string msg)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (Format, "ERROR", DateTime.Now.ToString ("g"), msg, string.Empty);
			Console.ForegroundColor = currentColor;
		}

        /// <summary>
        /// Errors the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="data">The data.</param>
        public static void Error(string msg, Dictionary<string, string> data)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (Format, "ERROR", DateTime.Now.ToString ("g"), msg, SerializeData (data));
			Console.ForegroundColor = currentColor;
		}

        /// <summary>
        /// Errors the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="data">The data.</param>
        public static void Error(string msg, Exception exception, Dictionary<string, string> data=null)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (Format, "ERROR", DateTime.Now.ToString ("g"), msg, SerializeData (data)+"\n\tException Message: " + exception.Message);
			Console.ForegroundColor = currentColor;
		}
	}
}