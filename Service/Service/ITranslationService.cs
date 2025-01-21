using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInstall.Service.Service
{
    public interface ITranslationService
    {
        Task<string> TranslateTextAsync(string text, string targetLanguage);
        Task<string> TranslateTextToBengaliAsync(string inputText);
        Task<string> TranslateTextAsync2(string inputText, string targetLanguage);
        Task<string> PerformTranslation(string text, string targetLanguage);
        Task<string> TranslateText(string text, string targetLanguage);
    }
}
