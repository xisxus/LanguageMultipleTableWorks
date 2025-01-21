using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LanguageInstall.Data.Data;
using LanguageInstall.Data.Model;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;




namespace LanguageInstall.Service.Service
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;


        public TranslationService(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<string> TranslateTextAsync(string text, string targetLanguage)
        {
            var requestBody = new
            {
                q = text,
                source = "en",
                target = targetLanguage,
                format = "text"
            };

            var response = await _httpClient.PostAsJsonAsync("https://libretranslate.com/translate", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent);
                return responseObject.GetProperty("translatedText").GetString();
            }
            else
            {
                throw new Exception("Translation API call failed.");
            }
        }


        


        public async Task<string> PerformTranslation(string text, string targetLanguage)
        {
            string result = string.Empty;

            // Configure Selenium WebDriver
            var options = new ChromeOptions();
            options.AddArgument("--headless"); // Run browser in headless mode
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    // Open Google Translate
                    string url = $"https://translate.google.com/?hl=en&sl=en&tl={targetLanguage}&op=translate";
                    driver.Navigate().GoToUrl(url);

                    // Wait for the page to load
                   // Thread.Sleep(2000);

                    // Locate the input box and enter the text
                    var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));
                    sourceTextBox.Clear();
                    sourceTextBox.SendKeys(text);

                    // Wait for the translation to complete
                    //Thread.Sleep(2000);

                    //// Retrieve the translated text
                    //var outputBox = driver.FindElement(By.XPath("//div[@class='J0lOec']"));
                    //result = outputBox.Text;

                    // Wait for translation with WebDriverWait instead of Thread.Sleep
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    Thread.Sleep(1000);
                    var translationContainer = wait.Until(d => d.FindElement(By.ClassName("ryNqvb")));
                    result = translationContainer.Text;
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

            return result;
        }

        public async Task<string> TranslateTextToBengaliAsync(string inputText)
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using var driver = new ChromeDriver(options);
            try
            {
                // Navigate to LibreTranslate website
                driver.Navigate().GoToUrl("https://libretranslate.com");

                Thread.Sleep(2000);
                // Create wait object
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait for and find the input textarea
                var sourceTextarea = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("textarea1")));
                sourceTextarea.Clear();
                sourceTextarea.SendKeys(inputText);

                Thread.Sleep(2000);
                // Wait for and find the target language dropdown (second dropdown for target language)
                var targetLangDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("(//select[@class='browser-default'])[2]")));

                // Click to open the dropdown
                targetLangDropdown.Click();
                Thread.Sleep(2000);
                // Select Bengali ("bn" is the language code for Bengali)
                var selectElement = new SelectElement(targetLangDropdown);
                selectElement.SelectByValue("bn");
                Thread.Sleep(2000);
                // Wait for translation to complete (looking for textarea2 which contains the result)
                var resultTextarea = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("textarea2")));

                // Add a small delay to ensure translation is complete
                await Task.Delay(5000);

                // Get the translated text
                string translatedText = resultTextarea.GetAttribute("value");

                // If text is empty, wait a bit longer and try again
                if (string.IsNullOrEmpty(translatedText))
                {
                    await Task.Delay(3000);
                    translatedText = resultTextarea.GetAttribute("value");
                }

                return translatedText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Bengali translation failed: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
        }


        public async Task<string> TranslateText(string text, string targetLanguage)
        {
            string translatedText = string.Empty;
            IWebDriver driver = new ChromeDriver();

            try
            {
                // Open Google Translate
                driver.Navigate().GoToUrl($"https://translate.google.com/?hl=en&sl=en&tl={targetLanguage}&op=translate");

                // Wait for the page to load
                Thread.Sleep(3000);

                // Locate and input text
                var sourceTextBox = driver.FindElement(By.XPath("//textarea[@aria-label='Source text']"));
                sourceTextBox.SendKeys(text);

                // Wait for the translation to process
                Thread.Sleep(2000);

                // Retrieve translated text
                var outputBox = driver.FindElement(By.XPath("//div[@class='J0lOec']"));
                translatedText = outputBox.Text;
            }
            finally
            {
                // Clean up
                driver.Quit();
            }

            return translatedText;
        }



        public async Task<string> TranslateTextAsync2(string inputText, string targetLanguage)
        {
            // Dictionary to map common language names to their codes
            Dictionary<string, string> languageCodes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"english", "en"},
                {"korean", "ko"},
                {"basque", "eu"},
                {"hungarian", "hu"},
                {"portuguese", "pt"},
                {"bulgarian", "bg"},
                {"czech", "cs"},
                {"spanish", "es"},
                {"german", "de"},
                {"chinese", "zh"},
                {"russian", "ru"},
                {"japanese", "ja"},
                {"esperanto", "eo"},
                {"kabyle", "kab"},
                {"french", "fr"},
                {"italian", "it"},
                {"ukrainian", "uk"}
            };

            // Try to get the language code, throw exception if language is not supported
            if (!languageCodes.TryGetValue(targetLanguage, out string? languageCode))
            {
                throw new ArgumentException($"Language '{targetLanguage}' is not supported. Supported languages are: {string.Join(", ", languageCodes.Keys)}");
            }

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using var driver = new ChromeDriver(options);
            try
            {
                // Navigate to LibreTranslate website
                // driver.Navigate().GoToUrl("https://www.google.com/search?q=google+translate&oq=google+tra&gs_lcrp=EgZjaHJvbWUqCQgBEAAYExiABDIGCAAQRRg5MgkIARAAGBMYgAQyCQgCEAAYExiABDIJCAMQABgTGIAEMgkIBBAAGBMYgAQyCQgFEAAYExiABDIJCAYQABgTGIAEMgkIBxAAGBMYgAQyCQgIEAAYExiABDIJCAkQABgTGIAE0gEINzk5MWowajeoAgCwAgA&sourceid=chrome&ie=UTF-8");
                driver.Navigate().GoToUrl("https://libretranslate.com");

                // Create wait object
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait for and find the input textarea
                var sourceTextarea = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("textarea1")));
                sourceTextarea.Clear();
                sourceTextarea.SendKeys(inputText);

                // Wait for and find the target language dropdown (second dropdown for target language)
                var targetLangDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("(//select[@class='browser-default'])[2]")));

                // Click to open the dropdown
                targetLangDropdown.Click();

                // Select the target language using the language code
                var selectElement = new SelectElement(targetLangDropdown);
                selectElement.SelectByValue(languageCode);

                // Wait for translation to complete
                var resultTextarea = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("textarea2")));

                // Add a small delay to ensure translation is complete
                await Task.Delay(2000);

                // Get the translated text
                string translatedText = resultTextarea.GetAttribute("value");

                // If text is empty, wait a bit longer and try again
                if (string.IsNullOrEmpty(translatedText))
                {
                    await Task.Delay(3000);
                    translatedText = resultTextarea.GetAttribute("value");
                }

                return translatedText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Translation to {targetLanguage} failed: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
        }


    }
}
