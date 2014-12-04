using System;
using System.Web.Http.Filters;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using Hangout.BusinessLogic;

namespace Hangout.WebAPI
{
	public class ExceptionHandlerFilter : ExceptionFilterAttribute
	{

		private readonly Dictionary<string, HttpStatusCode> _cailExceptionToHttpStatusCodes;
		private static MediaTypeFormatter MediaTypeFormatter;// = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

		public ExceptionHandlerFilter(MediaTypeFormatter mediaTypeFormatter)
		{

			MediaTypeFormatter = mediaTypeFormatter;

			_cailExceptionToHttpStatusCodes = new Dictionary<string, HttpStatusCode>();

			// Register mapping
			_cailExceptionToHttpStatusCodes[typeof(NotFoundException).FullName] = HttpStatusCode.NotFound;
			_cailExceptionToHttpStatusCodes[typeof(DuplicateEntityException).FullName] = HttpStatusCode.Conflict;
		}

		// TODO: We need to add log
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{

			var businessException = actionExecutedContext.Exception as BusinessException;
			if (businessException != null)
			{
				var statusCode = HttpStatusCode.BadRequest;
				if (_cailExceptionToHttpStatusCodes.ContainsKey(actionExecutedContext.Exception.GetType().FullName))
					statusCode = _cailExceptionToHttpStatusCodes[actionExecutedContext.Exception.GetType().FullName];

				actionExecutedContext.Response =
					CreateCailErrorResponseMessage (businessException, statusCode);
			}
			else if (actionExecutedContext.Exception is NotImplementedException)
			{
				actionExecutedContext.Response = CreateStandardErrorResponseMessage("This function has not been implemented yet.",
					HttpStatusCode.NotImplemented);
			}
			else
			{
				actionExecutedContext.Response = CreateStandardErrorResponseMessage("Oops! something happened on our side.",
					HttpStatusCode.InternalServerError);
			}

			base.OnException(actionExecutedContext);
		}

		private static HttpResponseMessage CreateStandardErrorResponseMessage(string message, HttpStatusCode httpStatusCode)
		{
			return new HttpResponseMessage(httpStatusCode)
			{
				Content = new ObjectContent<ErrorModel>(
					new ErrorModel
					{
						ErrorCode = (int)httpStatusCode,
						Message = message
					}, MediaTypeFormatter)
			};
		}

		private static HttpResponseMessage CreateCailErrorResponseMessage(BusinessException cailException, HttpStatusCode httpStatusCode)
		{
			var serializableObject = cailException.ToSerializableObject();
			var responseObject = new ObjectContent(serializableObject.GetType(), serializableObject,
				MediaTypeFormatter);

			var response = new HttpResponseMessage(httpStatusCode)
			{
				Content = responseObject
			};

			return response;
		}
	}
}