using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LanguageInstall.Data.Model
{
    public class MainTable
    {
        public int ID { get; set; }
        public string EnglishText { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
