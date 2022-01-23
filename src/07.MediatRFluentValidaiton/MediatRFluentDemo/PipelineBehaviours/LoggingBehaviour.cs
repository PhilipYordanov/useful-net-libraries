namespace MediatRFluentDemo.PipelineBehaviours
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
        
    using MediatR;

    using Microsoft.Extensions.Logging;

    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(
            ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            //Request
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            Type type = request.GetType();

            IList<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(request, null);
                _logger.LogInformation("{Property} : {@Value}", property.Name, propValue);
            }

            var response = await next();

            //Response
            _logger.LogInformation($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}
