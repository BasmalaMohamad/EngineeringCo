using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class ArabicTextHelper
    {
        public static string NormalizeArabicText(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string normalized = input.Normalize(NormalizationForm.FormD); // Decompose
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    switch (c)
                    {
                        case 'أ': case 'إ': case 'آ': sb.Append('ا'); break; // Alef
                        case 'ة': sb.Append('ه'); break; // Ta Marbuta to Ha
                        case 'ى': sb.Append('ي'); break; // Alef Maqsura to Ya
                        default: sb.Append(c); break;
                    }
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC); // Recompose
        }
    }
}
