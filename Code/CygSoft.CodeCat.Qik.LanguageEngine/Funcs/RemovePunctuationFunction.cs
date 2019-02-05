using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class RemovePunctuationFunction : BaseFunction
    {
        public RemovePunctuationFunction(FuncInfo funcInfo, GlobalTable scopeTable, List<BaseFunction> functionArguments)
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
                string txt = functionArguments[0].Execute(errorReport);

                if (txt != null && txt.Length >= 1)
                {
                    result = txt.Where(c => !char.IsPunctuation(c)).Aggregate("", (current, c) => current + c);
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


// http://stackoverflow.com/questions/421616/how-can-i-strip-punctuation-from-a-string
//string s = "sxrdct?fvzguh,bij.";
//var sb = new StringBuilder();

//foreach (char c in s)
//{
//   if (!char.IsPunctuation(c))
//      sb.Append(c);
//}

//s = sb.ToString();