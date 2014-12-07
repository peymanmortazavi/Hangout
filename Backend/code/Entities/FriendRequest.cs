using System;

namespace Hangout.Entities
{
	public class FriendRequest : IEntity
	{

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the requester identifier.
        /// </summary>
        /// <value>
        /// The requester identifier.
        /// </value>
        public string RequesterId { get; set; }

        /// <summary>
        /// Gets or sets the receiver identifier.
        /// </summary>
        /// <value>
        /// The receiver identifier.
        /// </value>
        public string ReceiverId { get; set; }

	}
}