using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Common.Behaviors;

public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly ILogger<LogginBehavior<TRequest, TResponse>> _logger;

    public LogginBehavior(ILogger<LogginBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        _logger.LogInformation("Handling {RequestName}",requestName);

        var response = await next();
        
        _logger.LogInformation("Handled {RequestName}", requestName);

        return response;
    }
}