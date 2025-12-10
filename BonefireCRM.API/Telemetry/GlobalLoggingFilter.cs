// <copyright file="GlobalLoggingFilter.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

namespace BonefireCRM.API.Telemetry
{
    public class GlobalLoggingFilter : IEndpointFilter
    {
        private readonly ILogger<GlobalLoggingFilter> _logger;

        public GlobalLoggingFilter(ILogger<GlobalLoggingFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            _logger.LogInformation("Starting {Endpoint}", context.HttpContext.Request.Path);

            var result = await next(context);

            _logger.LogInformation("Finished {Endpoint}", context.HttpContext.Request.Path);

            return result;
        }
    }
}
