using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine
{
    public class Compiler : ICompiler
    {
        public event EventHandler BeforeInput;
        public event EventHandler AfterInput;
        public event EventHandler BeforeCompile;
        public event EventHandler AfterCompile;
        public event EventHandler<CompileErrorEventArgs> CompileError;

        private ISyntaxValidator syntaxValidator = null;
        private ICompileEngine compileEngine = null;

        public bool HasErrors { get { return syntaxValidator.HasErrors || compileEngine.HasErrors; } }

        public string[] Symbols { get { return compileEngine.Symbols; } }
        public string[] Placeholders { get { return compileEngine.Placeholders; } }
        public IInputField[] InputFields { get { return compileEngine.InputFields; } }
        public IExpression[] Expressions { get { return compileEngine.Expressions; } }

        public Compiler()
        {
            syntaxValidator = new SyntaxValidator();
            compileEngine = new CompileEngine();
        }

        public Compiler(ISyntaxValidator syntaxValidator, ICompileEngine compileEngine)
        {
            this.syntaxValidator = syntaxValidator;
            this.compileEngine = compileEngine;
        }

        public void Compile(string scriptText)
        {
            CheckSyntax(scriptText);

            if (!syntaxValidator.HasErrors)
                CheckCompilation(scriptText);
            
        }

        public void Input(string symbol, string value)
        {
            compileEngine.BeforeInput += CompileEngine_BeforeInput;
            compileEngine.AfterInput += CompileEngine_AfterInput;

            compileEngine.Input(symbol, value);

            compileEngine.BeforeInput -= CompileEngine_BeforeInput;
            compileEngine.AfterInput -= CompileEngine_AfterInput;
        }

        public ISymbolInfo GetPlaceholderInfo(string placeholder)
        {
            return compileEngine.GetPlaceholderInfo(placeholder);
        }

        public ISymbolInfo GetSymbolInfo(string symbol)
        {
            return compileEngine.GetSymbolInfo(symbol);
        }

        public ISymbolInfo[] GetSymbolInfoSet(string[] symbols)
        {
            return compileEngine.GetSymbolInfoSet(symbols);
        }

        public string GetValueOfSymbol(string symbol)
        {
            return compileEngine.GetValueOfSymbol(symbol);
        }

        public string GetValueOfPlaceholder(string placeholder)
        {
            return compileEngine.GetValueOfPlaceholder(placeholder);
        }

        public string GetTitleOfPlaceholder(string placeholder)
        {
            return compileEngine.GetTitleOfPlaceholder(placeholder);
        }

        public string TextToSymbol(string text)
        {
            return "@" + text;
        }

        public string TextToPlaceholder(string text)
        {
            return "@{" + text + "}";
        }

        private void CheckCompilation(string scriptText)
        {
            compileEngine.BeforeCompile += Compiler_BeforeCompile;
            compileEngine.AfterCompile += Compiler_AfterCompile;
            compileEngine.CompileError += Compiler_CompileError;

            compileEngine.Compile(scriptText);

            compileEngine.CompileError -= Compiler_CompileError;
            compileEngine.BeforeCompile -= Compiler_BeforeCompile;
            compileEngine.AfterCompile -= Compiler_AfterCompile;
        }

        private void CheckSyntax(string scriptText)
        {
            syntaxValidator.CompileError += Compiler_CompileError;
            syntaxValidator.Validate(scriptText);
            syntaxValidator.CompileError -= Compiler_CompileError;
        }

        private void Compiler_AfterCompile(object sender, EventArgs e)
        {
            AfterCompile?.Invoke(this, e);
        }

        private void Compiler_BeforeCompile(object sender, EventArgs e)
        {
            BeforeCompile?.Invoke(this, e);
        }

        private void CompileEngine_AfterInput(object sender, EventArgs e)
        {
            AfterInput?.Invoke(this, e);
        }

        private void CompileEngine_BeforeInput(object sender, EventArgs e)
        {
            BeforeInput?.Invoke(this, e);
        }

        private void Compiler_CompileError(object sender, CompileErrorEventArgs e)
        {
            CompileError?.Invoke(this, e);
        }
    }
}
