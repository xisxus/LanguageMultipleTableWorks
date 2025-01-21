using LanguageInstall.Data.Data;
using LanguageInstall.Data.DTOs;
using LanguageInstall.Data.Model;
using LanguageInstall.Service.Service;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.SignalR;
using LanguageInstall.Service.SignalR;
using LanguageInstall.Service.Service.MultiTable;

namespace LanguageInstallMVC.Controllers
{
    public class TranslateController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ITranslationService _translationService;
        private readonly ILanguageTableService _languageTableService;
        private readonly ILocalizationService _localizationService;
        private readonly IHubContext<ProgressHub> _hubContext;

        public TranslateController(AppDbContext dbContext, ITranslationService translationService, IHubContext<ProgressHub> hubContext, ILanguageTableService languageTableService, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _translationService = translationService;
            _hubContext = hubContext;
            _languageTableService = languageTableService;
            _localizationService = localizationService;
        }



        // Index View
        public async Task<IActionResult> Index()
        {
            var data = await _dbContext.MainTables
                .Include(mt => mt.Translations)
                .Select(mt => new MainTableDto
                {
                    ID = mt.ID,
                    EnglishText = mt.EnglishText,
                    Translations = mt.Translations.ToDictionary(t => t.LanguageCode, t => t.TranslatedText)
                })
                .ToListAsync();

            return View(data);
        }

        // Translate View
        public IActionResult Translate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Translate(string text, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(targetLanguage))
            {
                ViewBag.Error = "Text and target language are required.";
                return View();
            }

            try
            {
                var translatedText = await _translationService.PerformTranslation(text, targetLanguage);
                ViewBag.TranslatedText = translatedText;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return View();
        }



        public IActionResult TranslateToMultipleInd()
        {

            

            var distinctTranslationCodes = _dbContext.Translation
                .Select(trans => trans.LanguageCode)
                .Distinct();

            var result = _dbContext.LanguageLists.ToList();
            var s = _localizationService.GetLangInd();
            ViewBag.AvLangInd = s.Result;

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> TranslateToMultipleInd(string languageCode, string translationQuality)
        {
            int sleepTime;

            switch (translationQuality)
            {
                case "Good":
                    sleepTime = 2000; // 1 second
                    break;
                case "Better":
                    sleepTime = 3000; // 2 seconds
                    break;
                case "Best":
                    sleepTime = 4000; // 3 seconds
                    break;
                default:
                    sleepTime = 2000; // Default to 2 seconds if no quality is selected
                    break;
            }

            int o = 1;

            await _languageTableService.AddTableWithInd(languageCode);

            var englishTexts = await _dbContext.MainTables
                .Include(mt => mt.Translations)
                .Where(mt => !mt.Translations.Any(t => t.LanguageCode == languageCode)).ToListAsync();

            int total = englishTexts.Count;
            ViewBag.total = total;

            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    string url = $"https://translate.google.com/?hl=en&sl=en&tl={languageCode}&op=translate";
                    driver.Navigate().GoToUrl(url);

                    Thread.Sleep(sleepTime);

                    var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));

                    foreach (var item in englishTexts)
                    {
                        sourceTextBox.Clear();
                        sourceTextBox.SendKeys(item.EnglishText);
                        Thread.Sleep(sleepTime + 1000);

                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var translationContainer = wait.Until(d => d.FindElement(By.ClassName("usGWQd")));
                        var result = translationContainer.Text;

                        var tableDataIndDto = new TableDataIndDto
                        {
                            EngText = item.EnglishText,
                            LangCode = languageCode,
                            TranslateText = result,

                        };

                        await _languageTableService.SaveDataWithInd(tableDataIndDto);

                        // Send progress to the client
                        int progress = (o++ * 100) / total;
                        await _hubContext.Clients.All.SendAsync("UpdateProgress", progress, total);

                    }
                    // Notify clients when the operation is completed
                    await _hubContext.Clients.All.SendAsync("OperationCompleted");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error during translation: {ex.Message}");
                }
                finally
                {
                    driver.Quit();
                }
            }

            return RedirectToAction("TranslateToMultiple", "Translations");
        }



        // Translate All Records

        public IActionResult TranslateToMultiple()
        {

            //var result = _dbContext.LanguageLists
            //    .Where(lang => !_dbContext.Translation
            //    .Any(trans => trans.LanguageCode == lang.LanguageCode))
            //    .ToList();

            var distinctTranslationCodes = _dbContext.Translation
                .Select(trans => trans.LanguageCode)
                .Distinct();

            var result = _dbContext.LanguageLists.ToList();
            var s = _localizationService.GetLang();
            ViewBag.AvLang = s.Result;

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> TranslateToMultiple(string languageCode, string translationQuality)
        {
            int sleepTime;

            switch (translationQuality)
            {
                case "Good":
                    sleepTime = 2000; // 1 second
                    break;
                case "Better":
                    sleepTime = 3000; // 2 seconds
                    break;
                case "Best":
                    sleepTime = 4000; // 3 seconds
                    break;
                default:
                    sleepTime = 2000; // Default to 2 seconds if no quality is selected
                    break;
            }

            int o = 1;

            await _languageTableService.AddTableWithRef(languageCode);

            var englishTexts = await _dbContext.MainTables
                .Include(mt => mt.Translations)
                .Where(mt => !mt.Translations.Any(t => t.LanguageCode == languageCode)).ToListAsync();

            int total = englishTexts.Count;
            ViewBag.total = total;

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    string url = $"https://translate.google.com/?hl=en&sl=en&tl={languageCode}&op=translate";
                    driver.Navigate().GoToUrl(url);

                    Thread.Sleep(sleepTime);

                    var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));

                    foreach (var item in englishTexts)
                    {
                        sourceTextBox.Clear();
                        sourceTextBox.SendKeys(item.EnglishText);
                        Thread.Sleep(sleepTime);

                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var translationContainer = wait.Until(d => d.FindElement(By.ClassName("usGWQd")));
                        var result = translationContainer.Text;

                        var tableDataRefDto = new TableDataRefDto
                        {
                            MainId = item.ID,
                            LangCode = languageCode,
                            TranslateText = result,

                        };

                        await _languageTableService.SaveDataWithRef(tableDataRefDto);

                        // Send progress to the client
                        int progress = (o++ * 100) / total;
                        await _hubContext.Clients.All.SendAsync("UpdateProgress", progress, total);

                    }
                    // Notify clients when the operation is completed
                    await _hubContext.Clients.All.SendAsync("OperationCompleted");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error during translation: {ex.Message}");
                }
                finally
                {
                    driver.Quit();
                }
            }

            return RedirectToAction("TranslateToMultiple", "Translations");
        }

        public IActionResult TranslateAll()
        {
            
            //var result = _dbContext.LanguageLists
            //    .Where(lang => !_dbContext.Translation
            //    .Any(trans => trans.LanguageCode == lang.LanguageCode))
            //    .ToList();

            var distinctTranslationCodes = _dbContext.Translation
                .Select(trans => trans.LanguageCode)
                .Distinct();

            var result = _dbContext.LanguageLists .ToList();


            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> TranslateAll(string languageCode, string translationQuality)
        {
            int sleepTime;

            switch (translationQuality)
            {
                case "Good":
                    sleepTime = 2000; // 1 second
                    break;
                case "Better":
                    sleepTime = 3000; // 2 seconds
                    break;
                case "Best":
                    sleepTime = 4000; // 3 seconds
                    break;
                default:
                    sleepTime = 2000; // Default to 2 seconds if no quality is selected
                    break;
            }

            int o = 1;

            var englishTexts = await _dbContext.MainTables
                .Include(mt => mt.Translations)
                .Where(mt => !mt.Translations.Any(t => t.LanguageCode == languageCode)).ToListAsync();

            int total = englishTexts.Count;
            ViewBag.total = total;

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    string url = $"https://translate.google.com/?hl=en&sl=en&tl={languageCode}&op=translate";
                    driver.Navigate().GoToUrl(url);

                    Thread.Sleep(sleepTime);

                    var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));

                    foreach (var item in englishTexts)
                    {
                        sourceTextBox.Clear();
                        sourceTextBox.SendKeys(item.EnglishText);
                        Thread.Sleep(sleepTime);

                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var translationContainer = wait.Until(d => d.FindElement(By.ClassName("usGWQd")));
                        var result = translationContainer.Text;

                        var translation = new Translation
                        {
                            MainTableID = item.ID,
                            LanguageCode = languageCode,
                            TranslatedText = result,
                           
                        };

                        _dbContext.Translation.Add(translation);
                        await _dbContext.SaveChangesAsync();

                        // Send progress to the client
                        int progress = (o++ * 100) / total;
                        await _hubContext.Clients.All.SendAsync("UpdateProgress", progress, total);

                    }
                    // Notify clients when the operation is completed
                    await _hubContext.Clients.All.SendAsync("OperationCompleted");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error during translation: {ex.Message}");
                }
                finally
                {
                    driver.Quit();
                }
            }

            return RedirectToAction("Index", "Translations");
        }


        //best backupu
        //[HttpPost]
        //public async Task<IActionResult> TranslateAll(string languageCode)
        //{
        //    var sleepTime = 2000;
        //    int o = 1;

        //    var englishTexts = await _dbContext.MainTables
        //        .Include(mt => mt.Translations)
        //        .Where(mt => !mt.Translations.Any(t => t.LanguageCode == languageCode)).ToListAsync();

        //    ViewBag.total = englishTexts.Count;

        //    var options = new ChromeOptions();
        //    // options.AddArgument("--headless"); // Run browser in headless mode
        //    options.AddArgument("--disable-gpu");
        //    options.AddArgument("--no-sandbox");

        //    using (var driver = new ChromeDriver(options))
        //    {
        //        try
        //        {
        //            // Open Google Translate
        //            string url = $"https://translate.google.com/?hl=en&sl=en&tl={languageCode}&op=translate";
        //            driver.Navigate().GoToUrl(url);

        //            Thread.Sleep(sleepTime);
        //           // Thread.Sleep(1000);

        //            var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));

        //            foreach (var item in englishTexts)
        //            {

        //                // Locate the input box and enter the text

        //                sourceTextBox.Clear();
        //                sourceTextBox.SendKeys(item.EnglishText);

        //                // Wait for the translation to complete
        //                Thread.Sleep(sleepTime);
        //               // Thread.Sleep(1000);

        //                //// Retrieve the translated text
        //                //var outputBox = driver.FindElement(By.XPath("//div[@class='J0lOec']"));
        //                //result = outputBox.Text;

        //                // Wait for translation with WebDriverWait instead of Thread.Sleep
        //                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //                Thread.Sleep(sleepTime);
        //                // Thread.Sleep(1000);

        //                // var translationContainer = wait.Until(d => d.FindElement(By.ClassName("ryNqvb")));
        //                var translationContainer = wait.Until(d => d.FindElement(By.ClassName("usGWQd")));
        //                var result = translationContainer.Text;

        //                var translation = new Translation
        //                {
        //                    MainTableID = item.ID,
        //                    LanguageCode = languageCode,
        //                    TranslatedText = result,
        //                };

        //                _dbContext.Translation.Add(translation);
        //                await _dbContext.SaveChangesAsync();
        //                ViewBag.progress = o++;
        //            }

        //            // Wait for the page to load

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception($"Error during translation: {ex.Message}");
        //        }
        //        finally
        //        {
        //            driver.Quit();
        //        }
        //    }


        //    //ViewBag.ExecutionTime = $"Execution Time: {stopwatch.Elapsed.Minutes} minute(s) {stopwatch.Elapsed.Seconds} second(s)";
        //    return RedirectToAction("Index", "Translations");

        //}







        //BackUp It work
        //[HttpPost]
        //public async Task<IActionResult> TranslateAll(string languageCode)
        //{

        //        var englishTexts = await _dbContext.MainTables
        //            .Include(mt => mt.Translations)
        //            .Where(mt => !mt.Translations.Any(t => t.LanguageCode == languageCode)).ToListAsync();


        //    var options = new ChromeOptions();
        //       // options.AddArgument("--headless"); // Run browser in headless mode
        //        options.AddArgument("--disable-gpu");
        //        options.AddArgument("--no-sandbox");

        //        using (var driver = new ChromeDriver(options))
        //        {
        //            try
        //            {
        //                // Open Google Translate
        //                string url = $"https://translate.google.com/?hl=en&sl=en&tl={languageCode}&op=translate";
        //                driver.Navigate().GoToUrl(url);

        //                foreach (var item in englishTexts)
        //                {
        //                    Thread.Sleep(1000);

        //                    // Locate the input box and enter the text
        //                    var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));
        //                    sourceTextBox.Clear();
        //                    sourceTextBox.SendKeys(item.EnglishText);

        //                    // Wait for the translation to complete
        //                    Thread.Sleep(1000);

        //                    //// Retrieve the translated text
        //                    //var outputBox = driver.FindElement(By.XPath("//div[@class='J0lOec']"));
        //                    //result = outputBox.Text;

        //                    // Wait for translation with WebDriverWait instead of Thread.Sleep
        //                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //                    Thread.Sleep(1000);
        //                   // var translationContainer = wait.Until(d => d.FindElement(By.ClassName("ryNqvb")));
        //                    var translationContainer = wait.Until(d => d.FindElement(By.ClassName("usGWQd")));
        //                    var result = translationContainer.Text;

        //                    var translation = new Translation
        //                    {
        //                        MainTableID = item.ID,
        //                        LanguageCode = languageCode,
        //                        TranslatedText = result,
        //                    };

        //                    _dbContext.Translation.Add(translation);
        //                    await _dbContext.SaveChangesAsync();
        //                }

        //                // Wait for the page to load

        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception($"Error during translation: {ex.Message}");
        //            }
        //            finally
        //            {
        //                driver.Quit();
        //            }
        //        }


        //    //ViewBag.ExecutionTime = $"Execution Time: {stopwatch.Elapsed.Minutes} minute(s) {stopwatch.Elapsed.Seconds} second(s)";
        //    return RedirectToAction("Index", "Translations");

        //}

        //[HttpPost]
        //public async Task<IActionResult> TranslateAll(string languageCode)
        //{
        //    const int batchSize = 1;
        //    bool hasMoreRows;

        //    do
        //    {
        //        // Fetch 15 rows at a time that don't have a translation for the requested language
        //        var batch = await _dbContext.MainTables
        //            .Include(mt => mt.Translations)
        //            .Where(mt => !mt.Translations.Any(t => t.LanguageCode == languageCode))
        //            .Take(batchSize)
        //            .ToListAsync();

        //        hasMoreRows = batch.Any();

        //        if (!hasMoreRows) break;

        //        // Extract English texts
        //        var englishTexts = batch.Select(record => record.EnglishText).ToList();

        //        // Merge texts using a separator
        //        var mergedText = string.Join(", ", englishTexts); // Use "||" as a unique separator

        //        // Translate the batch
        //        var translatedTex = await _translationService.PerformTranslation(mergedText, languageCode);

        //        var translatedTexts = translatedTex.Split(", ").ToList();

        //        // Map the translations back to the records and save
        //        for (int i = 0; i < batch.Count; i++)
        //        {
        //            var record = batch[i];
        //            var translatedText = translatedTexts[i];

        //            // Save the translation
        //            var translation = new Translation
        //            {
        //                MainTableID = record.ID,
        //                LanguageCode = languageCode,
        //                TranslatedText = translatedText
        //            };

        //            _dbContext.Translation.Add(translation);
        //        }

        //        // Save changes to the database
        //        await _dbContext.SaveChangesAsync();

        //    } while (hasMoreRows);

        //    //ViewBag.ExecutionTime = $"Execution Time: {stopwatch.Elapsed.Minutes} minute(s) {stopwatch.Elapsed.Seconds} second(s)";
        //    return RedirectToAction("Index", "Translations");

        //}

        //[HttpPost]
        //public async Task<IActionResult> TranslateAll(string languageCode)
        //{
        //    if (string.IsNullOrEmpty(languageCode))
        //    {
        //        ViewBag.Error = "Language code is required.";
        //        return View("Error");
        //    }

        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    var allRecords = await _dbContext.MainTables
        //        .Include(mt => mt.Translations)
        //        .ToListAsync();

        //    foreach (var record in allRecords)
        //    {
        //        if (record.Translations.Any(t => t.LanguageCode == languageCode))
        //            continue;

        //        var translatedText = await _translationService.TranslateTextAsync(record.EnglishText, languageCode);

        //        var translation = new Translation
        //        {
        //            MainTableID = record.ID,
        //            LanguageCode = languageCode,
        //            TranslatedText = translatedText
        //        };
        //        _dbContext.Translation.Add(translation);
        //    }

        //    await _dbContext.SaveChangesAsync();
        //    stopwatch.Stop();

        //    ViewBag.ExecutionTime = $"Execution Time: {stopwatch.Elapsed.Minutes} minute(s) {stopwatch.Elapsed.Seconds} second(s)";
        //    return View("TranslateAllSuccess");
        //}

        // Add Language Column
        public async Task<IActionResult> AddLanguageColumn(int id, string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                return View("Error", "Language code is required.");
            }

            var mainRecord = await _dbContext.MainTables.Include(mt => mt.Translations).FirstOrDefaultAsync(mt => mt.ID == id);

            if (mainRecord == null)
                return View("Error", "Record not found.");

            if (mainRecord.Translations.Any(t => t.LanguageCode == languageCode))
                return View("Error", "Translation already exists for this language.");

            var translatedText = await _translationService.TranslateTextAsync(mainRecord.EnglishText, languageCode);

            var translation = new Translation
            {
                MainTableID = mainRecord.ID,
                LanguageCode = languageCode,
                TranslatedText = translatedText
            };
            _dbContext.Translation.Add(translation);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
