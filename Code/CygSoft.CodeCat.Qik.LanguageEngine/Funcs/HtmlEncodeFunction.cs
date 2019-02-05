using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class HtmlEncodeFunction : BaseFunction
    {
        public HtmlEncodeFunction(FuncInfo funcInfo, GlobalTable scopeTable, List<BaseFunction> functionArguments) : base(funcInfo, scopeTable, functionArguments)
        {

        }
        public override string Execute(IErrorReport errorReport)
        {
            if (functionArguments.Count() != 1)
                errorReport.AddError(new CustomError(this.Line, this.Column, "Too many arguments", this.Name));

            string result = null;
            try
            {
                string txt = functionArguments[0].Execute(errorReport);

                if (txt != null && txt.Length >= 1)
                {
                    result = HttpUtility.HtmlEncode(txt); 
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
