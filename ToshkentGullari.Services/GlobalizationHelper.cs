using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToshkentGullari.Services
{
    public static class GlobalizationHelper
    {
        public static void SetLanguage(String lang)
        {
           
                lang = lang ?? "en-US";
                Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            
        }
    }
}
