using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine
{
    public class BatchCompiler : IBatchCompiler
    {
        public event EventHandler BeforeInput;
        public event EventHandler AfterInput;
        public event EventHandler BeforeCompile;
        public event EventHandler AfterCompile;
        public event EventHandler<CompileErrorEventArgs> CompileError;

        private ISyntaxValidator syntaxValidator = null;
        private ICompileEngine compileEngine = null;

        public bool HasErrors { get { return syntaxValidator.HasErrors; } }

        public string[] Placeholders { get { return compileEngine.Placeholders; } }
        public IExpression[] Expressions { get { return compileEngine.Expressions; } }

        public BatchCompiler()
        {
            syntaxValidator = new SyntaxValidator();
            compileEngine = new CompileEngine();
        }

        public BatchCompiler(ISyntaxValidator syntaxValidator, ICompileEngine compileEngine)
        {
            this.syntaxValidator = syntaxValidator;
            this.compileEngine = compileEngine;
        }

        public string SymbolFromField(string fieldName)
        {
            return "@" + fieldName;
        }

        public void CreateFieldInput(string symbol, string fieldName, string description)
        {
            compileEngine.CreateFieldInput(symbol, fieldName, description);
        }

        public void Compile(string scriptText)
        {
            CheckSyntax(scriptText);
            if (!syntaxValidator.HasErrors)
            {
                CheckCompilation(scriptText);
            }
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

        public void Input(string symbol, string fieldValue)
        {
            compileEngine.BeforeInput += CompileEngine_BeforeInput;
            compileEngine.AfterInput += CompileEngine_AfterInput;

            compileEngine.Input(symbol, fieldValue);

            compileEngine.BeforeInput -= CompileEngine_BeforeInput;
            compileEngine.AfterInput -= CompileEngine_AfterInput;
        }

        public ISymbolInfo GetSymbolInfo(string symbol)
        {
            return compileEngine.GetSymbolInfo(symbol);
        }

        public ISymbolInfo[] GetSymbolInfoSet(string[] symbols)
        {
            return compileEngine.GetSymbolInfoSet(symbols);
        }

        public string GetValueOfPlaceholder(string placeholder)
        {
            return compileEngine.GetValueOfPlaceholder(placeholder);
        }

        // And this is for ???
        private void CheckExecution()
        {
            foreach (IExpression expression in this.Expressions)
            {
                string value = expression.Value;
            }
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
