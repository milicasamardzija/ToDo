using MediatR;

namespace NovaLite.ToDo
{
    public class LoggingPipeline<TReq, TRes> : IPipelineBehavior<TReq, TRes> where TReq : IRequest<TRes>
    {
        private readonly ILogger<LoggingPipeline<TReq, TRes>> _logger;

        public LoggingPipeline(ILogger<LoggingPipeline<TReq, TRes>> logger)
        {
            _logger = logger;
        }

        public async Task<TRes> Handle(TReq request, CancellationToken cancellationToken, RequestHandlerDelegate<TRes> next)
        {
            var requestName = typeof(TReq).Name;
            _logger.LogInformation("Executing {RequestName}.", requestName);
            _logger.LogDebug("{RequestName} payload:\n{@RequestPayload}", requestName, request);
            var response = await next();
            _logger.LogInformation("{RequestName} executed.", requestName);
            _logger.LogDebug("{RequestName} response payload:\n{@ResponsePayload}", requestName, response);
            return response;
        }
    }
}
