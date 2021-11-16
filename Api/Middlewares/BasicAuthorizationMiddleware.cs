using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    /// <summary>
    /// 1.2.2**.3 - Middleware, который запрещает делать запрос, без хедера “Authorization” со значением “Basic admin:admin”
    /// </summary>
    public class BasicAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuthorizationMiddleware> _logger;

        public BasicAuthorizationMiddleware(RequestDelegate next, ILogger<BasicAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers["Authorization"] != "Basic admin:admin")
            {
                context.Response.StatusCode = 401;
                return;
            }
            await _next(context);
        }
    }
}
