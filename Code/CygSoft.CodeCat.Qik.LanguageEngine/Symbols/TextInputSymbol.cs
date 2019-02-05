using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Symbols
{
    internal class TextInputSymbol : InputSymbol, ITextField
    {
        private string value = null;

        public TextInputSymbol(IErrorReport errorReport, string symbol, string title, string description, string defaultValue, 
            bool isPlaceholder)
            : base(errorReport, symbol, title, description, defaultValue, isPlaceholder)
        {
            this.value = defaultValue;
        }

        public TextInputSymbol(IErrorReport errorReport, string symbol, string title, string description, string defaultValue, bool isPlaceholder,
            string prefix, string postfix)
            : base(errorReport, symbol, title, description, defaultValue, isPlaceholder, prefix, postfix)
        {
            this.value = defaultValue;
        }

        public override string Value
        {
            get { return this.value; }
        }

        public void SetValue(string value)
        {
            this.value = value;
        }
    }
}
