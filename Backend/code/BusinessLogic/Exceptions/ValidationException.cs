using Hangout.BusinessLogic;
using System.Collections.Generic;
using System.Linq;

namespace Hangout.BusinessLogic
{

	public class ValidationException : BusinessException
	{
		public IEnumerable<ValidationErrorDetail> Details { get; private set; }

		public ValidationException(int errorCode, string field, string fieldMessage, string message) : base(errorCode, message)
		{
			Details = new[] { new ValidationErrorDetail { Field = field, Message = fieldMessage } };
		}

		public ValidationException(int errorCode, Dictionary<string, string> details, string message)
			: base(errorCode, message)
		{
			Details = details.Select (x => new ValidationErrorDetail{ Message = x.Value, Field = x.Key });
		}

		public override object ToSerializableObject()
		{
			dynamic @object = base.ToSerializableObject();
			@object.details = Details.Select(x=>x.ToSerializableObject());
			return @object;
		}
	}

}