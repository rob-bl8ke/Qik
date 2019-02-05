using Antlr4.Runtime;
using CygSoft.CodeCat.Qik.LanguageEngine.Antlr;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine
{
    public class SyntaxValidator : ISyntaxValidator
    {
        public event EventHandler<CompileErrorEventArgs> CompileError;

        private GlobalTable scopeTable = new GlobalTable();
        private IErrorReport errorReport = new ErrorReport();

        public bool HasErrors { get; private set; }

        public SyntaxValidator()
        {
            HasErrors = false;
        }

        public void Validate(string scriptText)
        {
            HasErrors = false;

            AntlrInputStream inputStream = new AntlrInputStream(scriptText);
            QikTemplateLexer lexer = new QikTemplateLexer(inputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            QikTemplateParser parser = new QikTemplateParser(tokens);

            ErrorListener errorListener = new ErrorListener();
            errorListener.SyntaxErrorDetected += errorListener_SyntaxErrorDetected;
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);
            parser.template();
            errorListener.SyntaxErrorDetected -= errorListener_SyntaxErrorDetected;
        }

        private void errorReport_ExecutionErrorDetected(object sender, CompileErrorEventArgs e)
        {
            HasErrors = true;
            CompileError?.Invoke(this, e);
        }

        private void errorListener_SyntaxErrorDetected(object sender, CompileErrorEventArgs e)
        {
            HasErrors = true;
            CompileError?.Invoke(this, e);
        }
    }
}
