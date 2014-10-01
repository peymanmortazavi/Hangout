
namespace Hangout.Entities
{
	public class User : IEntity
	{

		public string Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string[] Friends { get; set; }

		public string[] AttendingEvents { get; set; }

	}


}