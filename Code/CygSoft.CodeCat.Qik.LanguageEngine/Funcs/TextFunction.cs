using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class TextFunction : BaseFunction
    {
        private string text;

        internal TextFunction(FuncInfo funcInfo, GlobalTable scopeTable, string text) : base(funcInfo, scopeTable)
        {
            this.text = text;
        }

        public override string Execute(IErrorReport errorReport)
        {
            return this.text;
        }
    }
}
