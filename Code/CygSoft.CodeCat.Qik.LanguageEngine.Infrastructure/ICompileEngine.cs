using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure
{
    public interface ICompileEngine
    {
        event EventHandler BeforeInput;
        event EventHandler AfterInput;
        event EventHandler BeforeCompile;
        event EventHandler AfterCompile;
        event EventHandler<CompileErrorEventArgs> CompileError;

        string[] Placeholders { get; }
        string[] Symbols { get; }
        IInputField[] InputFields { get; }
        IExpression[] Expressions { get; }

        bool HasErrors { get; }

        void CreateFieldInput(string symbol, string fieldName, string description);

        void Compile(string scriptText);
        void Input(string symbol, string value);

        ISymbolInfo GetSymbolInfo(string symbol);
        ISymbolInfo GetPlaceholderInfo(string placeholder);
        ISymbolInfo[] GetSymbolInfoSet(string[] symbols);
        string GetValueOfSymbol(string symbol);
        string GetValueOfPlaceholder(string placeholder);
        string GetTitleOfPlaceholder(string placeholder);
    }
}
