using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hangout.WebAPI
{
	public class ErrorModel
	{
		[JsonProperty("msg")]
		public string Message { get; set; }

		[JsonProperty("code")]
		public int ErrorCode { get; set; }
	}

	public class ValidationErrorModel
	{
		[JsonProperty("msg")]
		public string Message { get; set; }

		[JsonProperty("code")]
		public int ErrorCode { get; set; }

		public IEnumerable<ValidationErrorDetailModel> Details { get; set; } 
	}

	public class ValidationErrorDetailModel
	{
	
		public string Field { get; set; }

		[JsonProperty("msg")]
		public string Message { get; set; }

	}
}