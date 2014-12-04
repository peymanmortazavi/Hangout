using Hangout.BusinessLogic;

namespace Hangout.BusinessLogic
{

	public class DuplicateEntityException : BusinessException
	{
		private readonly string _field;

		public DuplicateEntityException(int errorCode, string field, string message) : base(errorCode, message)
		{
			this._field = field;
		}

		public override object ToSerializableObject ()
		{
			dynamic @object =  base.ToSerializableObject ();
			@object.field = _field;
			return @object;
		}
	}

}