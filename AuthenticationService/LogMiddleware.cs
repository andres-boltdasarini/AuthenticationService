namespace AuthenticationService
{
public class LogMiddleware 
{
  private readonly ILogger _logger;
  private readonly RequestDelegate _next;

  public LogMiddleware(RequestDelegate next, ILogger logger) 
  {
    _next = next;
    _logger = logger;
  }

        public async Task Invoke(HttpContext httpContext) 
        {
            // Получаем IP-адрес клиента
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            
            // Записываем IP-адрес в логгер
            _logger.WriteEvent($"IP-адрес клиента: {ipAddress}");
            
            await _next(httpContext);
        }
}
}
