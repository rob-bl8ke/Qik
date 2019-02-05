namespace CygSoft.CodeCat.Qik.LanguageEngine
{
    internal class Common
    {
        public static string StripOuterQuotes(string text)
        {
            if (text != null && text.Length >= 2)
            {
                if (text.Substring(0, 1) == "\"" && text.Substring(text.Length - 1, 1) == "\"")
                {
                    if (text.Length != 0)
                        return text.Substring(1, text.Length - 2);
                }
            }

            return text;
        }
    }
}
