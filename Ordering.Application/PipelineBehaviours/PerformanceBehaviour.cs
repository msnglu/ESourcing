using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ordering.Application.PipelineBehaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour( ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();
            var elepsedMilliSeconds = _timer.ElapsedMilliseconds;
            if(elepsedMilliSeconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("Long Runnig Request : {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    requestName,
                    elepsedMilliSeconds,
                    request);

            }
            return response;
        }
    }
}
