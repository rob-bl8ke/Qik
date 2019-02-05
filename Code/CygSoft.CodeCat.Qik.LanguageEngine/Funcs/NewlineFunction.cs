using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class NewlineFunction : BaseFunction
    {
        internal NewlineFunction(FuncInfo funcInfo)
            : base(funcInfo, null)
        {
        }

        public override string Execute(IErrorReport errorReport)
        {
            return Environment.NewLine;
        }
    }
}
