using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageInstall.Data.Model;

namespace LanguageInstall.Service.Service.MultiTable
{
    public interface ILanguageTableService
    {
        string GetConnectionString();
        Task AddLanguageAsync(string languageCode);
        Task AddTableWithRef(string languageCode);
        Task<bool> TableExistsWithRef(string languageCode);
        Task<string> GetTableNameWithRef(string languageCode);
        Task<string> GetTranslateWithRef(string tableName, int MainId);
        Task SaveDataWithRef(TableDataRefDto model);
        Task AddTableWithInd(string languageCode);
        Task<bool> TableExistsWithInd(string languageCode);
        Task<string> GetTableNameWithInd(string languageCode);
        Task<string> GetTranslateWithInd(string tableName, string key);
        Task SaveDataWithInd(TableDataIndDto model);
    }
}
