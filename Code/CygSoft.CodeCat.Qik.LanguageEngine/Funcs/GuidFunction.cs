using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class GuidFunction : BaseFunction
    {
        public GuidFunction(FuncInfo funcInfo, GlobalTable scopeTable, List<BaseFunction> functionArguments) : base(funcInfo, scopeTable, functionArguments)
        {

        }

        public override string Execute(IErrorReport errorReport)
        {
            if (functionArguments.Count() != 1)
                errorReport.AddError(new CustomError(this.Line, this.Column, "Guid function requires a single empty string parameter.", this.Name));

            string result = null;
            try
            {
                string txt = functionArguments[0].Execute(errorReport);
                result = txt == "u" ? Guid.NewGuid().ToString().ToUpper() : Guid.NewGuid().ToString();
            }
            catch (Exception)
            {
                errorReport.AddError(new CustomError(this.Line, this.Column, "Bad function call.", this.Name));
            }
            return result;
        }
    }
}
