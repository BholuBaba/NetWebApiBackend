using Microsoft.AspNetCore.Diagnostics;

namespace ReactWebApi.Exceptions
{
	public class GlobalExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			//if we want to add other types of exception in same exception class then need to do as below...
			//if (exception is NotImplementedException)
			//	return false;
			//if (exception is ArithmeticException)
			//	return false;

			//just to formating the Exception in better way
			var response = new ErrorResponse()
			{
				StatusCode = StatusCodes.Status500InternalServerError,
				Title = "Something Went Wrong!",
				ExceptionMessage = exception.Message
			};

			await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			return true;
		}
	}
}
