using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions
{
    /// <summary>
    /// Handles exceptions and generates appropriate problem details responses.
    /// </summary>
    public class ProblemExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemExceptionHandler"/> class.
        /// </summary>
        /// <param name="problemDetailsService">The service used to generate problem details responses.</param>
        public ProblemExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            ArgumentNullException.ThrowIfNull(problemDetailsService);
            _problemDetailsService = problemDetailsService;
        }

        /// <summary>
        /// Tries to handle the specified exception and generate a problem details response.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a boolean value indicating whether the exception was handled.
        /// </returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            if (exception is ConflictException conflictException)
            {
                var conflictDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = conflictException.Error,
                    Detail = conflictException.Message,
                    Type = "Conflict"
                };

                httpContext.Response.StatusCode = conflictDetails.Status.Value;
                return await _problemDetailsService.TryWriteAsync(
                    new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = conflictDetails,
                    }
                );
            }

            if (exception is ValidationException validationException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = validationException.Error,
                    Detail = validationException.Message,
                    Type = "Bad Request"
                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
                return await _problemDetailsService.TryWriteAsync(
                    new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = problemDetails,
                    }
                );
            }

            return true;
        }
    }
}