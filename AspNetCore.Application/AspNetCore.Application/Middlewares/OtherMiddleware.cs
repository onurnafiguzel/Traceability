namespace AspNetCore.Application.Middlewares;

public class OtherMiddleware(RequestDelegate next)
{
	public async Task Invoke(HttpContext context, ILogger<OtherMiddleware> logger)
	{
		var correlationID = context.Request.Headers["x-Correlation-ID"].FirstOrDefault();
		// same result
		correlationID = context.Items["CorrelationId"].ToString();

		NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationID);

		logger.LogDebug("OtherMiddlewareLog");

		await next(context);
	}
}
