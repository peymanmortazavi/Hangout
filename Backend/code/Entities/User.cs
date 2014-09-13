using System;
using System.Collections.Generic;

namespace Hangout.Entities
{
	public class User :IEntity
	{

		public class UserGroup
		{
			public string GroupId { get; set; }
			public bool IsHidden { get; set; }
		}

		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public List<string> Friends { get; set; }
		public List<UserGroup> Groups { get; set; }

	}


}