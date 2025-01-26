var builder = WebApplication.CreateBuilder(args);
//registramos las librerias carter, mapster , mediaTr
builder.Services.AddCarter();
//registramos los servicios de MediaTr y configuramos sus servicios 
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
var app = builder.Build();
///se construye el middleware de carter 
app.MapCarter();

app.Run();
