using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInstall.Data.DTOs
{
    public class MainTableDto
    {
        public int ID { get; set; }
        public string EnglishText { get; set; }
        public Dictionary<string, string> Translations { get; set; }
    }

}
