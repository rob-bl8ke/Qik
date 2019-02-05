using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;
using System.Collections.Generic;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class IfDecissionFunction : BaseFunction
    {
        private Dictionary<string, BaseFunction> functions = new Dictionary<string, BaseFunction>();
        private List<string> options = new List<string>();

        private string symbol = null;

        internal IfDecissionFunction(FuncInfo funcInfo, GlobalTable scopeTable, string symbol)
            : base(funcInfo, scopeTable)
        {
            this.symbol = symbol;
            this.scopeTable = scopeTable;
        }

        public override string Execute(IErrorReport errorReport)
        {
            string result = null;
            try
            {
                string curOption = scopeTable.GetValueOfSymbol(this.symbol);
                if (curOption != null && functions.ContainsKey(curOption))
                {
                    BaseFunction func = functions[curOption];
                    string txt = func.Execute(errorReport);

                    result = txt;
                }
            }
            catch (Exception)
            {
                errorReport.AddError(new CustomError(this.Line, this.Column, "If statement failed.", this.Name));
            }
            return result;
        }

        public void AddFunction(string text, BaseFunction func)
        {
            functions.Add(Common.StripOuterQuotes(text), func);
        }
    }
}
