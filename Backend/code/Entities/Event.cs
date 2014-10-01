using System;

namespace Hangout.Entities
{
	public class Event : IEntity
	{

		public string Id { get; set; }

		public string CreatorId { get; set; }

		public DateTime EventDate { get; set; }

		public string[] Users { get; set; }

		public Message[] Messages { get; set; }

	}

}

