using AioCore.Application;
using AioCore.Application.Helpers;
using AioCore.Domain.Common;
using AioCore.Services.AutomationServices;
using MediatR;

const string assemblyName = "AioCore.Web";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var appSettings = new AppSettings();
appSettings.Update(assemblyName!, "");
configuration.Bind(appSettings);

services.AddSingleton(appSettings);
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAioContext(appSettings);
services.AddAllSingleton(appSettings);
services.AddApiScoped(appSettings);
services.AddAllScenarios();
services.AddMediatR(typeof(Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();