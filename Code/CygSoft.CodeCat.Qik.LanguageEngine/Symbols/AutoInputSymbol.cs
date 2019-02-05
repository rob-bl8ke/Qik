using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Symbols
{
    internal class AutoInputSymbol : InputSymbol
    {
        private string value = null;

        public AutoInputSymbol(IErrorReport errorReport, string symbol, string title, string description) : base(errorReport, symbol, title, description, null, true)
        {
            this.value = null;
        }

        public AutoInputSymbol(IErrorReport errorReport, string symbol, string title, string description, string prefix, string postfix)
            : base(errorReport, symbol,title, description, null, true, prefix, postfix)
        {
            this.value = null;
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
