var builder = WebApplication.CreateBuilder(args);
//registramos las librerias carter, mapster , mediaTr
builder.Services.AddCarter();
//registramos los servicios de MediaTr y configuramos sus servicios 
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();
///se construye el middleware de carter 
app.MapCarter();

app.Run();
