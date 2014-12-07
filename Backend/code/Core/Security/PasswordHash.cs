using System;

namespace Hangout.Core
{
    /// <summary>
    /// Password hashing class
    /// </summary>
    public static class PasswordHash
	{

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string HashPassword(string password)
		{

			var data = System.Text.Encoding.UTF8.GetBytes (password);

			return Convert.ToBase64String (data);

		}

	}
}