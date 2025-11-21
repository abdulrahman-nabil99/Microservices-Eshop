using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[START] Handle request = {typeof(TRequest).Name} - response = {typeof(TResponse).Name} - requestData = {request}");
            var sp = Stopwatch.StartNew();
            var response = await next();
            sp.Stop();
            if (sp.ElapsedMilliseconds > 3000)
            {
                _logger.LogWarning($"[PERFORMANCE] The request={request} took {sp.ElapsedMilliseconds}ms");
            }
            return response;
        }
    }
}
