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
    public class UpperCaseFunctionTests
    {
        [Test]
        public void UpperCaseFunction_InputLower_OutputsUpperCase_1()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW

            GlobalTable globalTable = new GlobalTable();

            List<BaseFunction> functionArguments = new List<BaseFunction>();
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "literaltext"));

            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@classInstance", "Class Instance", "Description", true, true, new UpperCaseFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments));
            Assert.AreEqual("@classInstance", expressionSymbol.Symbol);
            Assert.AreEqual("@{classInstance}", expressionSymbol.Placeholder);
            Assert.AreEqual("Class Instance", expressionSymbol.Title);

            Assert.AreEqual("LITERALTEXT", expressionSymbol.Value);
        }

        [Test]
        public void UpperCaseFunction_InputLower_OutputsUpperCase()
        {
            string funcText = $"upperCase(\"lowercase text\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("LOWERCASE TEXT", output);
        }
    }
}
