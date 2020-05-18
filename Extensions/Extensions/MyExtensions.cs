using System.Text;

namespace Extensions
{
    public static class MyExtensions
    {
        public static string ToCamelCase(this string input) => ToCamelCaseInternal(input);
        private static string ToCamelCaseInternal(string s)
        {
            var newString = new StringBuilder();
            var sawUnderscore = false;

            foreach (var c in s)
            {
                if (newString.Length == 0 && char.IsLetter(c)) newString.Append(char.ToUpper(c));
                else if (c == '_') sawUnderscore = true;
                else if (sawUnderscore)
                {
                    newString.Append(char.ToUpper(c));
                    sawUnderscore = false;
                }
                else newString.Append(char.ToLower(c));
            }
            return newString.ToString();
        }
        public static string ToStringSafe<T>(this T obj) where T : class => obj == null ? null : obj.ToString();
    }
}
