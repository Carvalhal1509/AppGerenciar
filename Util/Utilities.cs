using System.Globalization;
using System.Text;

namespace DesafioSenaiCimatec.Util
{
    public class Utilities
    {
        public static string RemoveSpecialCharacters(string str)
        {
            if (str.Contains('\''))
            {
                str = str.Replace("\'", " ");
            }

            str = AccentInsensitive(str);
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '-' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string AccentInsensitive(string texto)
        {
            var normalizedString = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}