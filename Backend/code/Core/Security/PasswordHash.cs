using System;


namespace Hangout.Core
{
	public static class PasswordHash
	{

		public static string HashPassword(string password)
		{

			var data = System.Text.Encoding.UTF8.GetBytes (password);

			return Convert.ToBase64String (data);

		}

	}
}