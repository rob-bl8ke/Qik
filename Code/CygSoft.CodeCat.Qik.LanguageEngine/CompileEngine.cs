using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CygSoft.CodeCat.Qik.LanguageEngine.Antlr;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using System;
using System.Linq;

namespace CygSoft.CodeCat.Qik.LanguageEngine
{
    public class CompileEngine : ICompileEngine
    {
        public event EventHandler BeforeInput;
        public event EventHandler AfterInput;
        public event EventHandler BeforeCompile;
        public event EventHandler AfterCompile;
        public event EventHandler<CompileErrorEventArgs> CompileError;

        private GlobalTable scopeTable = new GlobalTable();
        private IErrorReport errorReport = new ErrorReport();

        public bool HasErrors { get; private set; }

        public string[] Symbols { get { return scopeTable.Symbols; } }

        public IInputField[] InputFields { get { return scopeTable.InputFields; } }
        public IExpression[] Expressions { get { return scopeTable.Expressions; } }

        public string[] Placeholders { get { return scopeTable.Placeholders; } }

        public CompileEngine()
        {
            HasErrors = false;
        }

        public void CreateFieldInput(string symbol, string fieldName, string description)
        {
            HasErrors = false;

            AutoInputSymbol autoInputSymbol = new AutoInputSymbol(this.errorReport, symbol, fieldName, description);
            if (!scopeTable.Symbols.Contains(autoInputSymbol.Symbol))
                scopeTable.AddSymbol(autoInputSymbol);
        }

        public void Input(string symbol, string value)
        {
            HasErrors = false;

            BeforeInput?.Invoke(this, new EventArgs());

            scopeTable.Input(symbol, value);

            AfterInput?.Invoke(this, new EventArgs());
        }

        public void Compile(string scriptText)
        {
            HasErrors = false;
            BeforeCompile?.Invoke(this, new EventArgs());

            try
            {
                scopeTable.Clear();

                errorReport.Reporting = true;
                errorReport.ExecutionErrorDetected += errorReport_ExecutionErrorDetected;

                CompileInputs(scriptText);
                CompileExpressions(scriptText);
                CheckExecution();

                errorReport.ExecutionErrorDetected -= errorReport_ExecutionErrorDetected;
                errorReport.Reporting = false;

                // this doesn't appear to be used...
                bool success = !this.errorReport.HasErrors;
                this.errorReport.Clear();
            }
            catch (Exception exception)
            {
                HasErrors = true;
                CompileError?.Invoke(this, new CompileErrorEventArgs(exception));
            }
            finally
            {
                AfterCompile?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// What is this doing here???
        /// </summary>
        private void CheckExecution()
        {
            foreach (IExpression expression in this.Expressions)
            {
                string value = expression.Value;
            }
        }

        private void CompileExpressions(string scriptText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(scriptText);
            QikTemplateLexer lexer = new QikTemplateLexer(inputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            QikTemplateParser parser = new QikTemplateParser(tokens);

            IParseTree tree = parser.template();

            ExpressionVisitor expressionVisitor = new ExpressionVisitor(this.scopeTable, this.errorReport);
            expressionVisitor.Visit(tree);
        }

        private void CompileInputs(string scriptText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(scriptText);
            QikTemplateLexer lexer = new QikTemplateLexer(inputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            QikTemplateParser parser = new QikTemplateParser(tokens);

            IParseTree tree = parser.template();

            UserInputVisitor controlVisitor = new UserInputVisitor(this.scopeTable, this.errorReport);
            controlVisitor.Visit(tree);
        }

        private void errorReport_ExecutionErrorDetected(object sender, CompileErrorEventArgs e)
        {
            HasErrors = true;
            CompileError?.Invoke(this, e);
        }

        public ISymbolInfo GetSymbolInfo(string symbol)
        {
            return scopeTable.GetSymbolInfo(symbol);
        }

        public ISymbolInfo GetPlaceholderInfo(string placeholder)
        {
            return scopeTable.GetPlaceholderInfo(placeholder);
        }

        public ISymbolInfo[] GetSymbolInfoSet(string[] symbols)
        {
            return scopeTable.GetSymbolInfoSet(symbols);
        }

        public string GetValueOfSymbol(string symbol)
        {
            return scopeTable.GetValueOfSymbol(symbol);
        }

        public string GetValueOfPlaceholder(string placeholder)
        {
            return scopeTable.GetValueOfPlacholder(placeholder);
        }

        public string GetTitleOfPlaceholder(string placeholder)
        {
            return scopeTable.GetTitleOfPlacholder(placeholder);
        }
    }
}
