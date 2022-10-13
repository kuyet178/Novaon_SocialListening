namespace AioCore.Shared.Common.Constants;

public static class SystemFeatures
{
    public const string Home = "";

    public const string Machines = "/machines";

    public const string Devices = "/devices";
    
    public static readonly List<string> Authorized = new()
    {
        "/_blazor",
        "/identity",
    };
}