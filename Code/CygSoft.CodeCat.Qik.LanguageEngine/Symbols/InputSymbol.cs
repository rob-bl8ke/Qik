using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Symbols
{
    internal abstract class InputSymbol : BaseSymbol, IInputField
    {
        public string DefaultValue { get; private set; }

        public InputSymbol(IErrorReport errorReport, string symbol, string title, string description, string defaultValue, 
            bool isPlaceholder)
            : base(errorReport, symbol, title, description, isPlaceholder)
        {
            this.DefaultValue = Common.StripOuterQuotes(defaultValue);
        }

        public InputSymbol(IErrorReport errorReport, string symbol, string title, string description, string defaultValue, bool isPlaceholder,  
            string prefix, string postfix)
            : base(errorReport, symbol, title, description, isPlaceholder, prefix, postfix)
        {
            this.DefaultValue = defaultValue;
        }
    }
}
