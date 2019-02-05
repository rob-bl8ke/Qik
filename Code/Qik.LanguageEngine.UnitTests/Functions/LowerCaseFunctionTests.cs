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
    public class LowerCaseFunctionTests
    {
        [Test]
        public void LowerCaseFunction_InputCAPS_OutputsLowerCase_1()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW

            GlobalTable globalTable = new GlobalTable();

            List<BaseFunction> functionArguments = new List<BaseFunction>();
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "LITERALTEXT"));

            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@classInstance", "Class Instance", "Description", true, true, new LowerCaseFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments));
            Assert.AreEqual("@classInstance", expressionSymbol.Symbol);
            Assert.AreEqual("@{classInstance}", expressionSymbol.Placeholder);
            Assert.AreEqual("Class Instance", expressionSymbol.Title);

            Assert.AreEqual("literaltext", expressionSymbol.Value);
        }

        [Test]
        public void LowerCaseFunction_InputCAPS_OutputsLowerCase()
        {
            string funcText = $"lowerCase(\"LOWERCASE TEXT\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("lowercase text", output);
        }
    }
}
