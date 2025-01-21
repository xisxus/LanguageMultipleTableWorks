using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageInstall.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LanguageInstall.Data.Data
{
    public static class SeedData
    {
        public static List<MainTable> MainTables => new List<MainTable>
        {
                new MainTable { ID = 1, EnglishText = "First Name" },
                new MainTable { ID = 2, EnglishText = "Last Name" },
                new MainTable { ID = 3, EnglishText = "Submit" },
                new MainTable { ID = 4, EnglishText = "Home" },
                new MainTable { ID = 5, EnglishText = "Human Resource Management" },
                new MainTable { ID = 6, EnglishText = "Leave Management" },
                new MainTable { ID = 7, EnglishText = "Operation" },
                new MainTable { ID = 8, EnglishText = "Leave Application Entry" },
                new MainTable { ID = 9, EnglishText = "Company" },
                new MainTable { ID = 10, EnglishText = "Employee ID" },
                new MainTable { ID = 11, EnglishText = "Employee Name" },
                new MainTable { ID = 12, EnglishText = "Designation" },
                new MainTable { ID = 13, EnglishText = "Department" },
                new MainTable { ID = 14, EnglishText = "Immediate Supervisor" },
                new MainTable { ID = 15, EnglishText = "Head of Department" },
                new MainTable { ID = 16, EnglishText = "Apply Leave Format" },
                new MainTable { ID = 17, EnglishText = "Leave Type" },
                new MainTable { ID = 18, EnglishText = "From" },
                new MainTable { ID = 19, EnglishText = "Day(s)" },
                new MainTable { ID = 20, EnglishText = "To" },
                new MainTable { ID = 21, EnglishText = "Half Day" },
                new MainTable { ID = 22, EnglishText = "First Half" },
                new MainTable { ID = 23, EnglishText = "Second Half" },
                new MainTable { ID = 24, EnglishText = "File Attachment" },
                new MainTable { ID = 25, EnglishText = "Reason" },
                new MainTable { ID = 26, EnglishText = "Leave Apply" },
                new MainTable { ID = 27, EnglishText = "Select Dropdown Options" },
                new MainTable { ID = 28, EnglishText = "--Select--" },
                new MainTable { ID = 29, EnglishText = "--Select Apply Leave Format--" },
                new MainTable { ID = 30, EnglishText = "Half Day Leave" },
                new MainTable { ID = 31, EnglishText = "Full Day Leave" },
                new MainTable { ID = 32, EnglishText = "Short Leave" },
                new MainTable { ID = 33, EnglishText = "--Select--" },
                new MainTable { ID = 34, EnglishText = "Button and Action Text" },
                new MainTable { ID = 35, EnglishText = "Leave Apply" },
                new MainTable { ID = 36, EnglishText = "Half Day" },
                new MainTable { ID = 37, EnglishText = "Full Day" },
                new MainTable { ID = 38, EnglishText = "Short Leave" },
                new MainTable { ID = 39, EnglishText = "Leave Duration" },
                new MainTable { ID = 40, EnglishText = "Select Employee" },
                new MainTable { ID = 41, EnglishText = "Select Leave Format" },
                new MainTable { ID = 42, EnglishText = "Select Leave Type" },
                new MainTable { ID = 43, EnglishText = "Sick Leave" },
                new MainTable { ID = 44, EnglishText = "Casual Leave" },
                new MainTable { ID = 45, EnglishText = "Select Leave Type" },
                new MainTable { ID = 46, EnglishText = "Enter Reason" },
                new MainTable { ID = 47, EnglishText = "Cancel" },
                new MainTable { ID = 48, EnglishText = "Entry ID" },
                new MainTable { ID = 49, EnglishText = "--Select Employee--" },
                new MainTable { ID = 50, EnglishText = "Additional Information" }

        };


        public static List<Translation> Translations => new List<Translation>
        {
            new Translation { ID = 1, MainTableID = 1, LanguageCode = "bn", TranslatedText = "প্রথম নাম" },
            new Translation { ID = 2, MainTableID = 2, LanguageCode = "bn", TranslatedText = "শেষ নাম" },
            new Translation { ID = 3, MainTableID = 3, LanguageCode = "bn", TranslatedText = "জমা দিন" },
            new Translation { ID = 4, MainTableID = 4, LanguageCode = "bn", TranslatedText = "বাড়ি" },
            new Translation { ID = 5, MainTableID = 5, LanguageCode = "bn", TranslatedText = "মানবসম্পদ পরিচালনা" },
            new Translation { ID = 6, MainTableID = 6, LanguageCode = "bn", TranslatedText = "ব্যবস্থাপনা ছেড়ে দিন" },
            new Translation { ID = 7, MainTableID = 7, LanguageCode = "bn", TranslatedText = "অপারেশন" },
            new Translation { ID = 8, MainTableID = 8, LanguageCode = "bn", TranslatedText = "অ্যাপ্লিকেশন এন্ট্রি ছেড়ে দিন" },
            new Translation { ID = 9, MainTableID = 9, LanguageCode = "bn", TranslatedText = "সংস্থা" },
            new Translation { ID = 10, MainTableID = 10, LanguageCode = "bn", TranslatedText = "কর্মচারী আইডি" },
            new Translation { ID = 11, MainTableID = 11, LanguageCode = "bn", TranslatedText = "কর্মচারীর নাম" },
            new Translation { ID = 12, MainTableID = 12, LanguageCode = "bn", TranslatedText = "উপাধি" },
            new Translation { ID = 13, MainTableID = 13, LanguageCode = "bn", TranslatedText = "বিভাগ" },
            new Translation { ID = 14, MainTableID = 14, LanguageCode = "bn", TranslatedText = "তাত্ক্ষণিক সুপারভাইজার" },
            new Translation { ID = 15, MainTableID = 15, LanguageCode = "bn", TranslatedText = "বিভাগের প্রধান" },
            new Translation { ID = 16, MainTableID = 16, LanguageCode = "bn", TranslatedText = "ছুটি ফর্ম্যাট প্রয়োগ করুন" },
            new Translation { ID = 17, MainTableID = 17, LanguageCode = "bn", TranslatedText = "ছেড়ে দিন" },
            new Translation { ID = 18, MainTableID = 18, LanguageCode = "bn", TranslatedText = "থেকে" },
            new Translation { ID = 19, MainTableID = 19, LanguageCode = "bn", TranslatedText = "দিন (গুলি)" },
            new Translation { ID = 20, MainTableID = 20, LanguageCode = "bn", TranslatedText = "থেকে" },
            new Translation { ID = 21, MainTableID = 21, LanguageCode = "bn", TranslatedText = "অর্ধ দিন" },
            new Translation { ID = 22, MainTableID = 22, LanguageCode = "bn", TranslatedText = "প্রথমার্ধ" },
            new Translation { ID = 23, MainTableID = 23, LanguageCode = "bn", TranslatedText = "দ্বিতীয়ার্ধ" },
            new Translation { ID = 24, MainTableID = 24, LanguageCode = "bn", TranslatedText = "ফাইল সংযুক্তি" },
            new Translation { ID = 25, MainTableID = 25, LanguageCode = "bn", TranslatedText = "কারণ" },
            new Translation { ID = 26, MainTableID = 26, LanguageCode = "bn", TranslatedText = "আবেদন করুন" },
            new Translation { ID = 27, MainTableID = 27, LanguageCode = "bn", TranslatedText = "ড্রপডাউন বিকল্পগুলি নির্বাচন করুন" },
            new Translation { ID = 28, MainTableID = 28, LanguageCode = "bn", TranslatedText = "-নির্বাচন-" },
            new Translation { ID = 29, MainTableID = 29, LanguageCode = "bn", TranslatedText = "-নির্বাচন করুন ছুটির ফর্ম্যাট প্রয়োগ করুন-" },
            new Translation { ID = 30, MainTableID = 30, LanguageCode = "bn", TranslatedText = "অর্ধ দিনের ছুটি" },
            new Translation { ID = 31, MainTableID = 31, LanguageCode = "bn", TranslatedText = "পুরো দিন ছুটি" },
            new Translation { ID = 32, MainTableID = 32, LanguageCode = "bn", TranslatedText = "সংক্ষিপ্ত ছুটি" },
            new Translation { ID = 33, MainTableID = 33, LanguageCode = "bn", TranslatedText = "-নির্বাচন-" },
            new Translation { ID = 34, MainTableID = 34, LanguageCode = "bn", TranslatedText = "বোতাম এবং ক্রিয়া পাঠ্য" },
            new Translation { ID = 35, MainTableID = 35, LanguageCode = "bn", TranslatedText = "আবেদন করুন" },
            new Translation { ID = 36, MainTableID = 36, LanguageCode = "bn", TranslatedText = "অর্ধ দিন" },
            new Translation { ID = 37, MainTableID = 37, LanguageCode = "bn", TranslatedText = "পুরো দিন" },
            new Translation { ID = 38, MainTableID = 38, LanguageCode = "bn", TranslatedText = "সংক্ষিপ্ত ছুটি" },
            new Translation { ID = 39, MainTableID = 39, LanguageCode = "bn", TranslatedText = "সময়কাল" },
            new Translation { ID = 40, MainTableID = 40, LanguageCode = "bn", TranslatedText = "কর্মচারী নির্বাচন করুন" },
            new Translation { ID = 41, MainTableID = 41, LanguageCode = "bn", TranslatedText = "ছুটি ফর্ম্যাট নির্বাচন করুন" },
            new Translation { ID = 42, MainTableID = 42, LanguageCode = "bn", TranslatedText = "ছুটি প্রকার নির্বাচন করুন" },
            new Translation { ID = 43, MainTableID = 43, LanguageCode = "bn", TranslatedText = "অসুস্থ ছুটি" },
            new Translation { ID = 44, MainTableID = 44, LanguageCode = "bn", TranslatedText = "নৈমিত্তিক ছুটি" },
            new Translation { ID = 45, MainTableID = 45, LanguageCode = "bn", TranslatedText = "ছুটি প্রকার নির্বাচন করুন" },
            new Translation { ID = 46, MainTableID = 46, LanguageCode = "bn", TranslatedText = "কারণ লিখুন" },
            new Translation { ID = 47, MainTableID = 47, LanguageCode = "bn", TranslatedText = "বাতিল" },
            new Translation { ID = 48, MainTableID = 48, LanguageCode = "bn", TranslatedText = "এন্ট্রি আইডি" },
            new Translation { ID = 49, MainTableID = 49, LanguageCode = "bn", TranslatedText = "-- কর্মচারী নির্বাচন করুন --" },
            new Translation { ID = 50, MainTableID = 50, LanguageCode = "bn", TranslatedText = "অতিরিক্ত তথ্য" }
        };


      
        public static List<LanguageList> LanguageLists =>  new List<LanguageList>
        {
            new LanguageList { ID = 1, LanguageCode = "en", LanguageName = "English" },
            new LanguageList { ID = 2, LanguageCode = "es", LanguageName = "Spanish" },
            new LanguageList { ID = 3, LanguageCode = "fr", LanguageName = "French" },
            new LanguageList { ID = 4, LanguageCode = "de", LanguageName = "German" },
            new LanguageList { ID = 5, LanguageCode = "zh", LanguageName = "Chinese" },
            new LanguageList { ID = 6, LanguageCode = "ja", LanguageName = "Japanese" },
            new LanguageList { ID = 7, LanguageCode = "ko", LanguageName = "Korean" },
            new LanguageList { ID = 8, LanguageCode = "ar", LanguageName = "Arabic" },
            new LanguageList { ID = 9, LanguageCode = "hi", LanguageName = "Hindi" },
            new LanguageList { ID = 10, LanguageCode = "ru", LanguageName = "Russian" },
            new LanguageList { ID = 11, LanguageCode = "pt", LanguageName = "Portuguese" },
            new LanguageList { ID = 12, LanguageCode = "it", LanguageName = "Italian" },
            new LanguageList { ID = 13, LanguageCode = "nl", LanguageName = "Dutch" },
            new LanguageList { ID = 14, LanguageCode = "sv", LanguageName = "Swedish" },
            new LanguageList { ID = 15, LanguageCode = "no", LanguageName = "Norwegian" },
            new LanguageList { ID = 16, LanguageCode = "da", LanguageName = "Danish" },
            new LanguageList { ID = 17, LanguageCode = "fi", LanguageName = "Finnish" },
            new LanguageList { ID = 18, LanguageCode = "pl", LanguageName = "Polish" },
            new LanguageList { ID = 19, LanguageCode = "tr", LanguageName = "Turkish" },
            new LanguageList { ID = 20, LanguageCode = "th", LanguageName = "Thai" },
            new LanguageList { ID = 21, LanguageCode = "vi", LanguageName = "Vietnamese" },
            new LanguageList { ID = 22, LanguageCode = "ms", LanguageName = "Malay" },
            new LanguageList { ID = 23, LanguageCode = "id", LanguageName = "Indonesian" },
            new LanguageList { ID = 24, LanguageCode = "el", LanguageName = "Greek" },
            new LanguageList { ID = 25, LanguageCode = "he", LanguageName = "Hebrew" },
            new LanguageList { ID = 26, LanguageCode = "ur", LanguageName = "Urdu" },
            new LanguageList { ID = 27, LanguageCode = "bn", LanguageName = "Bengali" },
            new LanguageList { ID = 28, LanguageCode = "ta", LanguageName = "Tamil" },
            new LanguageList { ID = 29, LanguageCode = "te", LanguageName = "Telugu" },
            new LanguageList { ID = 30, LanguageCode = "fa", LanguageName = "Persian" },
        };
        

    }
}
