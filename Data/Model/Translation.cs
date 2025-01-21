namespace LanguageInstall.Data.Model
{
    public class Translation
    {
        public int ID { get; set; }
        public int MainTableID { get; set; }
        public string LanguageCode { get; set; }
        public string TranslatedText { get; set; }

        public MainTable MainTable { get; set; }
    }
}