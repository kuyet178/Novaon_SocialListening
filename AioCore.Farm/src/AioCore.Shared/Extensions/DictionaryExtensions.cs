using System.Reflection;

namespace AioCore.Shared.Extensions;

public static class DictionaryExtensions
{
    public static Dictionary<string, object> ToDictionary(this object obj)
    {
        var dictionary = obj.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null));
        return dictionary!;
    }
}