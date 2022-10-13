using Newtonsoft.Json;

namespace AioCore.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public static T ParseTo<T>(this string input)
        {
            return JsonConvert.DeserializeObject<T>(input) ?? default!;
        }
    }
}