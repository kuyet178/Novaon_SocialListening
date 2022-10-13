using AioCore.Application.Helpers;
using AioCore.Domain.Common;
using AntDesign.ProLayout;
using MediatR;
using System.Reflection;

var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var appSettings = new AppSettings();
appSettings.Update(assemblyName!, assemblyPath);
configuration.Bind(appSettings);

services.AddSingleton(appSettings);
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddAntDesign();
services.Configure<ProSettings>(x =>
{
    x.NavTheme = "light";
    x.HeaderHeight = 48;
    x.Layout = "side";
    x.ContentWidth = "Fluid";
    x.FixedHeader = true;
    x.FixSiderbar = true;
    x.Title = "Ant Design Pro";
    x.IconfontUrl = null;
    x.PrimaryColor = "daybreak";
    x.ColorWeak = false;
    x.SplitMenus = true;
    x.HeaderRender = true;
    x.FooterRender = true;
    x.MenuRender = true;
    x.MenuHeaderRender = true;
});
services.AddAioContext(appSettings);
services.AddAllSingleton(appSettings);
services.AddWebScoped();
services.AddMediatR(typeof(Program).Assembly);
services.AddMediatR(typeof(AioCore.Application.Assembly).Assembly);

var app = builder.Build();

app.UseAioCoreDatabase(appSettings);
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});
app.Run();