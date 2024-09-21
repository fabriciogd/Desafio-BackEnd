using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Extensions;
using System.Diagnostics;

namespace Moto.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var commandName = request.GetGenericTypeName();

            logger.LogInformation("Handling command '{CommandName}'", commandName);

            var timer = new Stopwatch();
            timer.Start();

            try
            {
                var response = await next();

                timer.Stop();

                var timeTaken = timer.Elapsed.TotalSeconds;
                logger.LogInformation("Command '{CommandName}' handled ({TimeTaken} seconds)", commandName, timeTaken);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Command '{CommandName}' exception ({Exception})", commandName, ex);

                throw;
            }
        }
    }
}
