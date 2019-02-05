using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using LanguageEngine.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace LanguageEngine.Tests.UnitTests.Functions
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Functions")]
    [Category("Tests.UnitTests")]
    class ConcatenateFunctionTests
    {
        [Test]
        public void ConcatenateFunction_Input3Strings_ConcatenatesToSingleString_1()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW
            GlobalTable globalTable = new GlobalTable();

            ConcatenateFunction concatFunc = new ConcatenateFunction(new FuncInfo("stub", 1, 1), globalTable);

            concatFunc.AddFunction(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "hello"));
            concatFunc.AddFunction(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, " "));
            concatFunc.AddFunction(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "world"));

            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@concat", "Concatenated String", "Description", true, true, concatFunc);
            Assert.AreEqual("@concat", expressionSymbol.Symbol);
            Assert.AreEqual("@{concat}", expressionSymbol.Placeholder);
            Assert.AreEqual("Concatenated String", expressionSymbol.Title);

            Assert.AreEqual("hello world", expressionSymbol.Value);
        }

        [Test]
        public void ConcatenateFunction_Input3Strings_ConcatenatesToSingleString()
        {
            string concatExpression = "\"hello\" + \" \" + \"world\"";
            string output = TestHelpers.EvaluateCompilerFunction(concatExpression);
            Assert.AreEqual("hello world", output);
        }
    }
}
