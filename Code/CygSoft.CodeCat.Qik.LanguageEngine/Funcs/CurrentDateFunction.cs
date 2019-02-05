using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class CurrentDateFunction : BaseFunction
    {
        internal CurrentDateFunction(FuncInfo funcInfo, GlobalTable scopeTable, List<BaseFunction> functionArguments)
            : base(funcInfo, scopeTable, functionArguments)
        {

        }

        public override string Execute(IErrorReport errorReport)
        {
            if (functionArguments.Count() != 1)
                errorReport.AddError(new CustomError(this.Line, this.Column, "Too many arguments", this.Name));

            string result = null;
            try
            {
                string dateFormatText = functionArguments[0].Execute(errorReport);

                if (dateFormatText != null && dateFormatText.Length >= 1)
                {
                    string dateText = DateTime.Now.ToString(dateFormatText);
                    result = dateText;
                }
            }
            catch (Exception)
            {
                errorReport.AddError(new CustomError(this.Line, this.Column, "Bad function call.", this.Name));
            }
            return result;
        }
    }
}
