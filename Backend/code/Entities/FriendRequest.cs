using System;

namespace Hangout.Entities
{
	public class FriendRequest : IEntity
	{

		public string Id { get; set; }

		public string RequesterId { get; set; }

		public string ReceiverId { get; set; }

	}
}