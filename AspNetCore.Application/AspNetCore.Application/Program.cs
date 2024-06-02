using AspNetCore.Application.Middlewares;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

#region Nlog Setup
builder.Logging.ClearProviders();
builder.Host.UseNLog();
#endregion

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>()
   .UseMiddleware<OtherMiddleware>();

app.MapGet("/", (HttpContext context, ILogger<Program> logger) =>
{
	var correlationID = context.Request.Headers["x-Correlation-ID"].FirstOrDefault();
	// same result
	correlationID = context.Items["CorrelationId"].ToString();

	NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationID);

	logger.LogDebug("Program.cs Minimal API Log"); 
});

app.Run();
