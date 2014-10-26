using Hangout.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

namespace Hangout.BusinessLogic
{

	public class ValidationErrorDetail
	{
		public string Field { get; set; }

		public string Message { get; set; }

		internal object ToSerializableObject()
		{
			dynamic @object = new ExpandoObject();
			@object.field = Field;
			@object.msg = Message;
			return @object;
		}
	}

}