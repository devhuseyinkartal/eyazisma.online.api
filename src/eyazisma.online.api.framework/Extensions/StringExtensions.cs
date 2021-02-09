using System.Text.RegularExpressions;

namespace eyazisma.online.api.framework.Extensions
{
    internal static class StringExtensions
    {
        public static string EncodePath(this string s)
        {
            s = s.Replace('ş', 's');
            s = s.Replace('Ş', 'S');
            s = s.Replace('ö', 'o');
            s = s.Replace('Ö', 'O');
            s = s.Replace('ç', 'c');
            s = s.Replace('Ç', 'C');
            s = s.Replace('i', 'i');
            s = s.Replace('İ', 'I');
            s = s.Replace('ı', 'i');
            s = s.Replace('I', 'I');
            s = s.Replace('ğ', 'g');
            s = s.Replace('Ğ', 'G');
            s = s.Replace('ü', 'u');
            s = s.Replace('Ü', 'U');
            s = new Regex("[^\\x20-\\x7e]").Replace(s, "");
            s = new Regex("\\s+").Replace(s, "");
            return s;
        }
    }
}
