using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markdown
{
    public static class Formatter 
    {
        public static string FormatGround(string text, string tokenOpen, string tokenClose)
        {
            if (IsOnlyDigitsBetweenTokens(text, "_"))
                return text;
            return Regex.Replace(text, "_(.*)_", $"{tokenOpen}$1{tokenClose}");
        }

        public static string FormatDoubleGround(string text, string tokenOpen, string tokenClose)
        {
            if (IsOnlyDigitsBetweenTokens(text, "__"))
                return text;
            return Regex.Replace(text, "__(.*)__", $"{tokenOpen}$1{tokenClose}");
        }
        public static string FormatGreaterAndLesser(string text, string tokenOpen, string tokenClose)
        {
            return Regex.Replace(text, "`(.*)`", $"{tokenOpen}$1{tokenClose}");
        }
        public static string FormatBacktick(string text, string tokenOpen, string tokenClose)
        {
            return Regex.Replace(text, "`(.*)`", $"{tokenOpen}$1{tokenClose}");
        }

        public static bool IsOnlyDigitsBetweenTokens(string text, string token)
        {
            string regexp = $"{token}(.*){token}";
            var data = Regex.Match(text, regexp);
            // CR (krait): 2. А почему не может быть так, что матча не будет?
            return data.Groups[1].Value.All(Char.IsDigit);
        }
    }
}
