using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EMusicAPI.Helper
{
    public class SeoFriendlyLink
    {


      
        public static string FriendlyURLTitle(string url)
        {
            url = url.Replace(" ", "-");
            url = url.Replace(".", "-");
            url = url.Replace("ı", "i");
            url = url.Replace("İ", "I");

            url = String.Join("", url.Normalize(NormalizationForm.FormD) // turkish  convert to english  
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            url = HttpUtility.UrlEncode(url);
            return System.Text.RegularExpressions.Regex.Replace(url, @"\%[0-9A-Fa-f]{2}", "");
        }

    }
}
