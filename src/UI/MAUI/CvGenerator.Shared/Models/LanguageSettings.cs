namespace PompeiiNovenaCalendar.Domain.Models
{
    public class LanguageSettings
    {
        public string Language { get; } 

        public LanguageSettings(string twoLetterISOLanguageName)
        {
            var language = twoLetterISOLanguageName?.ToLower() ?? string.Empty;

            if (language != "en" && language != "pl")
                language = "en";

            Language = language;
        }
    }
}
