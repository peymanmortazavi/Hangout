using System.Collections.Generic;


namespace Hangout.Entities
{
	public class User : IEntity
	{

		public string Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public List<string> Friends { get; set; }

		public List<string> AttendingEvents { get; set; }

	}


}