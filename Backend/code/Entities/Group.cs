﻿using System.Collections.Generic;

namespace Hangout.Entities
{
	public class Group : IEntity
	{

		public string Id { get; set; }

		public string Name { get; set; }

		public List<string> Members { get; set; }

	}
}