using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class VariableFunction : BaseFunction
    {
        private string symbol;

        public VariableFunction(FuncInfo funcInfo, GlobalTable scopeTable, string symbol)
            : base(funcInfo, scopeTable)
        {
            this.symbol = symbol;
        }

        public override string Execute(IErrorReport errorReport)
        {
            return base.scopeTable.GetValueOfSymbol(this.symbol);
        }
    }
}
