using System.Net.NetworkInformation;
using Tools;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Configure MCP server (only supported options)
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<CustomInstructionsTool>();

builder.Services.AddCors();

var app = builder.Build();

// Add CORS
app.UseCors(policy => 
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

// Use routing middleware
app.UseRouting();

// Add diagnostic middleware
app.Use(async (context, next) => 
{
    Console.WriteLine($"Received request: {context.Request.Method} {context.Request.Path}");
    await next();
});

// Use top-level route registration pattern (recommended for .NET 6+)
app.MapMcp();

// Add a health check endpoint
app.MapGet("/health", () => "MCP Server is running");

// Use the configuration from launchSettings.json instead of hardcoded value
app.Run();
