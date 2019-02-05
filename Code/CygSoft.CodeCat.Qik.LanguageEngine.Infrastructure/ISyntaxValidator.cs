using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure
{
    public interface ISyntaxValidator
    {
        event EventHandler<CompileErrorEventArgs> CompileError;
        bool HasErrors { get; }
        void Validate(string scriptText);
    }
}
