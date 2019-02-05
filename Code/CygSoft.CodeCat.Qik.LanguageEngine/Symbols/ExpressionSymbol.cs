using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Symbols
{
    internal class ExpressionSymbol : BaseSymbol, IExpression
    {
        private BaseFunction func;
        public bool IsVisibleToEditor { get; private set; }
        private IErrorReport errorReport;

        public ExpressionSymbol(IErrorReport errorReport, string symbol, string title, string description, 
            bool isPlaceholder, bool isVisibleToEditor, BaseFunction func)
            : base(errorReport, symbol, title, description, isPlaceholder)
        {
            this.func = func;
            this.IsVisibleToEditor = isVisibleToEditor;
            this.errorReport = errorReport;
        }

        public ExpressionSymbol(IErrorReport errorReport, string symbol, string title, string description, 
            bool isPlaceholder, bool isVisibleToEditor, BaseFunction func, string prefix, string postfix)
            : base(errorReport, symbol, title, description, isPlaceholder, prefix, postfix)
        {
            this.func = func;
            this.IsVisibleToEditor = IsVisibleToEditor;
            this.errorReport = errorReport;
        }

        public override string Value
        {
            get { return func.Execute(errorReport); }
        }
    }
}
