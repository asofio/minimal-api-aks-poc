using Carter;
using FluentValidation.AspNetCore;
using minimalApiAksPoc.Features.Orders.Services;
using minimalApiAksPoc.Features.Orders.Validators;

var builder = WebApplication.CreateBuilder(args);

// Authentication and Authorization can be added, configured and used here as you would normally see it in a typical ASP.NET Core project.
// For the sake of keeping this example simple, we're not adding any authentication or authorization.
// builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCarter();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OrderValidator>());
builder.Services.AddSingleton<IOrderService, OrderCache>();
builder.Logging.AddApplicationInsights();
var app = builder.Build();

app.Lifetime.ApplicationStopping.Register(() =>
{
    // ApplicationStopping called, sleeping for 20s to allow any async operations to complete
    // Handle shutdown here as is appropriate for your situation.  There is an excellent blog post here
    // that explains the choosing of waiting 20 seconds: https://blog.markvincze.com/graceful-termination-in-kubernetes-with-asp-net-core/
    Thread.Sleep(20000);
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapCarter();

app.MapGet("/hello", () => "Hello World from .NET 6 minimal API!")
.WithTags("Hello");

app.MapGet("/hello/personal/{name}", (string name, ILogger<WebApplication> log) => {
    log.LogInformation($"Hello {name}!");

    return $"Hello, {name}!";
})
.WithTags("Hello");

app.MapGet("/echo", (string message, ILogger<WebApplication> log) => {
    log.LogInformation($"Echoing {message}");

    return message;
})
.WithTags("Echo");

app.Run();
