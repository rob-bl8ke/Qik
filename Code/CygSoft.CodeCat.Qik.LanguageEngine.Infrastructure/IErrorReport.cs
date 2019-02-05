using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure
{
    public interface IErrorReport
    {
        event EventHandler<CompileErrorEventArgs> ExecutionErrorDetected;

        bool HasErrors { get; }
        bool Reporting { get; set; }
        CustomError[] Errors { get; }
        void AddError(CustomError error);
        void Clear();
    }
}
