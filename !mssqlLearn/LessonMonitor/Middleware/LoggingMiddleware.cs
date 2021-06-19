using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);

                //using (FileStream fs = new FileStream("Loggs", FileMode.OpenOrCreate))
                //{
                //    byte[] arr = Encoding.UTF8.GetBytes($"Request " +
                //        $"{context.Request?.Method} " +
                //        $"{context.Request?.Path.Value} => " +
                //        $"{context.Response?.StatusCode}");

                //    fs.Write(arr);
                //}

                using (StreamWriter sw = new StreamWriter("Loggs.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine($"Request " +
                        $"{context.Request?.Method} " +
                        $"{context.Request?.Path.Value} => " +
                        $"{context.Response?.StatusCode}" +
                        $" | Execute time => {DateTime.Now}");
                }
            }
        }
    }
}
