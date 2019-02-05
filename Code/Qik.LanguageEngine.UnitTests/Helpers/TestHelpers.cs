using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;

namespace LanguageEngine.Tests.UnitTests.Helpers
{
    public class TestHelpers
    {
        internal static string EvaluateCompilerFunction(string functionText)
        {
            Compiler compiler = new Compiler();
            string expression = TestHelpers.BuildExpressionForFunction(functionText);

            compiler.Compile(expression);
            string val = compiler.GetValueOfSymbol("@output");

            return val;
        }

        internal static OptionInputSymbol CreateOptionInputSymbol_DatabaseOptions(string symbol = "@databaseOptions", string title = "Database Options", string description = null, string defaultValue = null, bool isPlaceholder = true)
        {
            OptionInputSymbol optionInputSymbol = new OptionInputSymbol(new ErrorReport(), symbol, title, description, defaultValue, isPlaceholder);
            optionInputSymbol.AddOption("ADVWORKS", "Adventure Works Database");
            optionInputSymbol.AddOption("PUBBOOKS", "Published Books Database");

            return optionInputSymbol;
        }

        internal static IOptionsField CreateOptionInputSymbol_DatabaseOptions_AsInterface(string symbol = "@databaseOptions", string title = "Database Options", string description = null, string defaultValue = null, bool isPlaceholder = true)
        {
            OptionInputSymbol optionInputSymbol = new OptionInputSymbol(new ErrorReport(), symbol, title, description, defaultValue, isPlaceholder);
            optionInputSymbol.AddOption("ADVWORKS", "Adventure Works Database");
            optionInputSymbol.AddOption("PUBBOOKS", "Published Books Database");

            return optionInputSymbol as IOptionsField;
        }

        internal static TextInputSymbol CreateTextInputSymbol_Author(string symbol = "@authorName", string title = "Author Name", string description = null, string defaultValue = null, bool isPlaceholder = true)
        {
            TextInputSymbol textInputSymbol = new TextInputSymbol(new ErrorReport(), symbol, title, description, defaultValue, isPlaceholder);
            return textInputSymbol;
        }

        private static string BuildExpressionForFunction(string functionText)
        {
            return "@output = expression [Title=\"Expression Text\"] { return " + functionText + "; };";
        }
    }
}
