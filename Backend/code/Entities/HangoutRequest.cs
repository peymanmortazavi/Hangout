using System;

namespace Hangout.Entities
{
	public class HangoutRequest : IEntity
	{

		public string Id { get; set; }

		public string GroupId { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

	}
}