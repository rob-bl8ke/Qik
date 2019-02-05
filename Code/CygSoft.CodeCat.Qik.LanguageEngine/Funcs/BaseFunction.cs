using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System.Collections.Generic;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal abstract class BaseFunction
    {
        protected GlobalTable scopeTable = null;
        protected List<BaseFunction> functionArguments;

        public int Line { get; private set; }
        public int Column { get; private set; }
        public string Name { get; private set; }

        internal BaseFunction(FuncInfo funcInfo, GlobalTable scopeTable, List<BaseFunction> functionArguments)
        {
            this.Line = funcInfo.Line;
            this.Column = funcInfo.Column;
            this.Name = funcInfo.Name;
            this.scopeTable = scopeTable;
            this.functionArguments = functionArguments;
        }

        internal BaseFunction(FuncInfo funcInfo, GlobalTable scopeTable)
        {
            this.Line = funcInfo.Line;
            this.Column = funcInfo.Column;
            this.Name = funcInfo.Name;
            this.scopeTable = scopeTable;
            this.functionArguments = new List<BaseFunction>();
        }

        public abstract string Execute(IErrorReport errorReport);
    }
}
