using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Antlr
{
    internal class UserInputVisitor : QikTemplateBaseVisitor<string>
    {
        private GlobalTable scopeTable;
        private IErrorReport errorReport;

        internal UserInputVisitor(GlobalTable scopeTable, IErrorReport errorReport)
        {
            this.scopeTable = scopeTable;
            this.errorReport = errorReport;
        }

        public override string VisitTextBox(QikTemplateParser.TextBoxContext context)
        {
            string controlId = context.VARIABLE().GetText();

            SymbolArguments symbolArguments = new SymbolArguments(errorReport);
            symbolArguments.Process(context.declArgs());

            TextInputSymbol textInputSymbol = new TextInputSymbol(errorReport, controlId, symbolArguments.Title, symbolArguments.Description, symbolArguments.Default, symbolArguments.IsPlaceholder);
            scopeTable.AddSymbol(textInputSymbol);

            return base.VisitTextBox(context);
        }

        public override string VisitOptionBox(QikTemplateParser.OptionBoxContext context)
        {
            string symbol = context.VARIABLE().GetText();

            SymbolArguments symbolArguments = new SymbolArguments(errorReport);
            symbolArguments.Process(context.declArgs());

            OptionInputSymbol optionInputSymbol = new OptionInputSymbol(errorReport, symbol, symbolArguments.Title, symbolArguments.Description, symbolArguments.Default, symbolArguments.IsPlaceholder);

            foreach (QikTemplateParser.SingleOptionContext optionContext in context.optionsBody().singleOption())
            {
                SymbolArguments optionArgs = new SymbolArguments(errorReport);
                optionArgs.Process(optionContext.declArgs());

                optionInputSymbol.AddOption(Common.StripOuterQuotes(optionContext.STRING().GetText()),
                    optionArgs.Title, optionArgs.Description);
            }

            scopeTable.AddSymbol(optionInputSymbol);
            return base.VisitOptionBox(context);
        }
    }
}
