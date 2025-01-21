using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageInstall.Data.Data;
using LanguageInstall.Data.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LanguageInstall.Service.Service.MultiTable
{
    public class LanguageTableService : ILanguageTableService
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;


        public LanguageTableService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("con");
        }

        #region Junk
        public async Task AddLanguageAsync(string languageCode)
        {
            // Validate the language code
            if (string.IsNullOrWhiteSpace(languageCode))
                throw new ArgumentException("Language code cannot be empty.");

            // Check if the language already exists
            var exists = await _context.Database.ExecuteSqlRawAsync(
                $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Translations_{languageCode}'") > 0;

            if (exists)
                throw new InvalidOperationException($"Translations table for '{languageCode}' already exists.");

            // Add a record to LanguageMetadataTable
            _context.Database.ExecuteSqlRaw($"INSERT INTO LanguageMetadataTable (LanguageCode, LanguageName) VALUES ('{languageCode}', '{languageCode}')");

            // Create a new table for the language
            var createTableSql = $@"
            CREATE TABLE Translations_{languageCode} (
                Id INT PRIMARY KEY IDENTITY(1,1),
                MainTableId INT FOREIGN KEY REFERENCES MainTable(Id),
                TranslatedText NVARCHAR(MAX)
            )";
            await _context.Database.ExecuteSqlRawAsync(createTableSql);
        }
        #endregion

        #region Ref

        public async Task<bool> TableExistsWithRef(string languageCode)
        {
            var languageName = languageCode;

            var tableName = $"LanguageRef_{languageName}";

            // Validate the language code
            if (string.IsNullOrWhiteSpace(languageCode))
                throw new ArgumentException("Language code cannot be empty.");

            var existsSql1 = $@"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'
              ";

            // Check if the language already exists
            //var exists = await _context.Database.ExecuteSqlRawAsync(
            //    $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'") > 0;

            var exists = await _context.Database.ExecuteSqlRawAsync(existsSql1) > 0;

            if (exists)
            {
                return false;
            }
            return true;
        }
        public async Task AddTableWithRef(string languageCode)
        {
            // var languageName = await _context.LanguageLists.Where(e=>e.Equals(languageCode)).Select(r=>r.LanguageName).FirstOrDefaultAsync();
            var languageName = languageCode;

            var tableName = $"LanguageRef_{languageName}";

            // Validate the language code
            if (string.IsNullOrWhiteSpace(languageCode))
                throw new ArgumentException("Language code cannot be empty.");

            // Check if the language already exists
            var exists = await _context.Database.ExecuteSqlRawAsync(
                $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'") > 0;

            if (!exists)
            {

                // Create a new table for the language
                var createTableSql = $@"
                    CREATE TABLE {tableName} (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        MainTableId INT FOREIGN KEY REFERENCES MainTables(ID),
                        TranslatedText NVARCHAR(MAX)
                    )";
                await _context.Database.ExecuteSqlRawAsync(createTableSql);
            }
        }

        public async Task SaveDataWithRef(TableDataRefDto model)
        {
            try
            {

                // var languageName = _context.LanguageLists.Where(e => e.Equals(model.LangCode)).Select(r => r.LanguageName).FirstOrDefault();
                var languageName = model.LangCode;

                var tableName = $"LanguageRef_{languageName}";


                var insertSql = $@"
                INSERT INTO {tableName} (MainTableId, TranslatedText)
                VALUES ({model.MainId}, '{model.TranslateText}')";

                await _context.Database.ExecuteSqlRawAsync(insertSql);
                // await _context.Database.ExecuteSqlRawAsync(insertSql, new SqlParameter("@translatedText", translatedText));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> GetTableNameWithRef(string languageCode)
        {
            var tableName = $"LanguageRef_{languageCode}";
            return await Task.FromResult(tableName);
        }

        public async Task<string> GetTranslateWithRef(string tableName, int MainId)
        {
           

            

            var selectSql = $@"
                    SELECT TranslatedText FROM {tableName} WHERE MainTableId = @Key
                ";

          //  using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@Key", MainId);

                    var result = await command.ExecuteScalarAsync();
                    return result?.ToString();
                }
            }
        }


        #endregion


        #region Ind

        public async Task AddTableWithInd(string languageCode)
        {
            //var languageName = _context.LanguageLists.Where(e => e.Equals(languageCode)).Select(r => r.LanguageName).FirstOrDefault();

            var languageName = languageCode;

            var tableName = $"LanguageInd_{languageName}";

            // Validate the language code
            if (string.IsNullOrWhiteSpace(languageCode))
                throw new ArgumentException("Language code cannot be empty.");

            // Check if the language already exists
            var exists = await _context.Database.ExecuteSqlRawAsync(
                $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'") > 0;

            if (!exists)
            {

                // Create a new table for the language
                var createTableSql = $@"
                    CREATE TABLE {tableName} (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        EngText NVARCHAR(MAX),
                        TranslatedText NVARCHAR(MAX)
                    )";
                await _context.Database.ExecuteSqlRawAsync(createTableSql);
            }
        }


        public async Task SaveDataWithInd(TableDataIndDto model)
        {
            try
            {
                //var languageName = _context.LanguageLists.Where(e => e.Equals(model.LangCode)).Select(r => r.LanguageName).FirstOrDefault();
                var languageName = model.LangCode;

                var tableName = $"LanguageInd_{languageName}";

                var insertSql = $@"
                INSERT INTO {tableName} (MainTableId, TranslatedText)
                VALUES ({model.EngText}, {model.TranslateText})";

                await _context.Database.ExecuteSqlRawAsync(insertSql);
                // await _context.Database.ExecuteSqlRawAsync(insertSql, new SqlParameter("@translatedText", translatedText));
            }
            catch (Exception)
            {

                throw;
            }

        }



        public async Task<string> GetTableNameWithInd(string languageCode)
        {
            var tableName = $"LanguageInd_{languageCode}";
            return await Task.FromResult(tableName);
        }


        public Task<string> GetTranslateWithInd(string tableName, string key)
        {
            throw new NotImplementedException();
        }

        

        public async Task<bool> TableExistsWithInd(string languageCode)
        {
            var languageName = languageCode;

            var tableName = $"LanguageInd_{languageName}";

            // Validate the language code
            if (string.IsNullOrWhiteSpace(languageCode))
                throw new ArgumentException("Language code cannot be empty.");

            // Check if the language already exists
            var exists = await _context.Database.ExecuteSqlRawAsync(
                $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'") > 0;

            if (!exists)
            {
                return false;
            }
            return true;
        }



        #endregion

        public string  GetConnectionString()
        {
            string connectionString = _connectionString;
            return connectionString;
        }
    }

}
