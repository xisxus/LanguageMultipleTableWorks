using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LanguageInstall.Service.Service
{
    public static class TranslationHelper
    {
        public static string Translate(string key, HttpContext context, ILocalizationService service)
        {
            var languageCode = context.Items["Language"] as string ?? "en";
            return service.GetTranslationAsync(key, languageCode).Result;
        }
    }
}
