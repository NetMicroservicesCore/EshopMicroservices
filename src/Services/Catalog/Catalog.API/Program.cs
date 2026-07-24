var builder = WebApplication.CreateBuilder(args);
//registramos las librerias carter, mapster , mediaTr
builder.Services.AddCarter();
//CORS para Angular (http://localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
//registramos los servicios de MediaTr y configuramos sus servicios 
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
//registramos FluentValidation y el pipeline behavior
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            var result = System.Text.Json.JsonSerializer.Serialize(new { errors });
            await context.Response.WriteAsync(result);
        }
    });
});

app.UseCors();
///se construye el middleware de carter 
app.MapCarter();

app.Run();

public partial class Program { }
