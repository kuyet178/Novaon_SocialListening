namespace AioCore.Shared.Extensions;

public static class APIExtensions
{
    public static string Merge(params string[] @params)
    {
        return @params.JoinString("/");
    }
}