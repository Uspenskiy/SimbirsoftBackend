using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    /// <summary>
    /// Middleware, который будет логировать с помощью ILogger (стандартная реализация) время начала запроса и время завершения запроса.
    /// </summary>
    public class TimeRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeRequestMiddleware> _logger;
        private Stopwatch _stopWatch;

        public TimeRequestMiddleware(RequestDelegate next, ILogger<TimeRequestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _stopWatch = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _stopWatch.Start();
            await _next(context);
            _stopWatch.Stop();
            _logger.LogInformation($"Время запроса к {context.Request.Path} метод {context.Request.Method} : {_stopWatch.ElapsedMilliseconds} ms");
        }
    }
}
