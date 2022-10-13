using System.Security.Cryptography;
using System.Text;

namespace AioCore.Shared.Extensions
{
    public static class StringExtensions
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        public const string ValidChar = "_abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly string MachineName = Environment.MachineName;

        public static Guid ToGuid(this string str)
        {
            return Guid.TryParse(str, out var guid) ? guid : Guid.Empty;
        }

        public static bool ParseGuid(this string? str)
        {
            return Guid.TryParse(str, out _);
        }

        public static string CreateMd5(this string? input, string? prefix = "")
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
                sb.Append(hashByte.ToString("X2"));
            var prefixMd5 = !string.IsNullOrEmpty(prefix) ? $"{prefix}_" : string.Empty;
            return $"{prefixMd5}{sb}";
        }
        
        public static string JoinString(this IEnumerable<string>? arr, string character)
        {
            if (arr is null) return string.Empty;
            var enumerable = arr as string[] ?? arr.ToArray();
            return string.Join(character, enumerable);
        }
        
        public static byte[] ToBytes(this string input)
        {
            var bits = (from c in input.ToUpper().ToCharArray()
                select Convert.ToString(Alphabet.IndexOf(c), 2).PadLeft(5, '0')).Aggregate((string a, string b) => a + b);
            return (from i in Enumerable.Range(0, bits.Length / 8)
                select Convert.ToByte(bits.Substring(i * 8, 8), 2)).ToArray();
        }
    }
}