using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markdown
{
    public class Formatter 
    {
        public string FormatGround(string text, string tokenOpen, string tokenClose)
        {
            if (IsOnlyDigitsBetweenTokens(text, "_"))
                return text;
            return Regex.Replace(text, "_(.*)_", $"{tokenOpen}$1{tokenClose}");
        }

        public string FormatDoubleGround(string text, string tokenOpen, string tokenClose)
        {
            if (IsOnlyDigitsBetweenTokens(text, "__"))
                return text;
            return Regex.Replace(text, "__(.*)__", $"{tokenOpen}$1{tokenClose}");
        }
        public string FormatGreaterAndLesser(string text)
        {
            text = text.Replace("\\<", "&lt;");
            text = text.Replace("\\>", "&gt;");
            return text;
        }
        public string FormatBacktick(string text, string tokenOpen, string tokenClose)
        {
            return Regex.Replace(text, "`(.*)`", $"{tokenOpen}$1{tokenClose}");
        }

        public bool IsOnlyDigitsBetweenTokens(string text, string token)
        {
            string regexp = $"{token}(.*){token}";
            var data = Regex.Match(text, regexp);
            // CR (krait): 2. А почему не может быть так, что матча не будет?
            return data.Groups[1].Value.All(Char.IsDigit);
        }
    }
}
