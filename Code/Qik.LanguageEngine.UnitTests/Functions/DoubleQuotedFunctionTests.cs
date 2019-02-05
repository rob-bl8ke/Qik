using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using LanguageEngine.Tests.UnitTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace LanguageEngine.Tests.UnitTests.Functions
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Functions")]
    [Category("Tests.UnitTests")]
    public class DoubleQuotedFunctionTests
    {
        [Test]
        public void DoubleQuoteFunction_InputText_OutputsDoubleQuotedText()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW

            GlobalTable globalTable = new GlobalTable();

            List<BaseFunction> functionArguments = new List<BaseFunction>();
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "literal text"));

            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@classInstance", "Class Instance", "Description", true, true, new DoubleQuoteFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments));
            Assert.AreEqual("@classInstance", expressionSymbol.Symbol);
            Assert.AreEqual("@{classInstance}", expressionSymbol.Placeholder);
            Assert.AreEqual("Class Instance", expressionSymbol.Title);

            Assert.AreEqual("\"literal text\"", expressionSymbol.Value);
        }

        [Test]
        public void DoubleQuoteFunction_Old_InputText_OutputsDoubleQuotedText()
        {
            string funcText = $"doubleQuotes(\"quote me\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("\"quote me\"", output);
        }

        [Test]
        public void DoubleQuoteFunction_New_InputText_OutputsDoubleQuotedText()
        {
            string funcText = $"doubleQuote(\"quote me\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("\"quote me\"", output);
        }
    }
}