using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class FloatFunction : BaseFunction
    {
        private string text;

        public FloatFunction(FuncInfo funcInfo, GlobalTable scopeTable, string text)
            : base(funcInfo, scopeTable)
        {
            this.text = text;
        }

        public override string Execute(IErrorReport errorReport)
        {
            return text;
        }
    }
}
