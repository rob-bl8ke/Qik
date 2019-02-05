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
    class ReplaceFunctionTests
    {
        [Test]
        public void ReplaceFunction_InputText_ReplacesCorrectly_1()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW

            GlobalTable globalTable = new GlobalTable();

            List<BaseFunction> functionArguments = new List<BaseFunction>();
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "Dashboard Usage"));
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, " "));
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "_"));

            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@classInstance", "Class Instance", "Description", true, true, new ReplaceFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments));
            Assert.AreEqual("@classInstance", expressionSymbol.Symbol);
            Assert.AreEqual("@{classInstance}", expressionSymbol.Placeholder);
            Assert.AreEqual("Class Instance", expressionSymbol.Title);

            Assert.AreEqual("Dashboard_Usage", expressionSymbol.Value);
        }

        [Test]
        public void ReplaceFunction_InputText_ReplacesCorrectly()
        {
            string funcText = $"replace(\"literal text ya all\", \" \", \"_\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("literal_text_ya_all", output);
        }
    }
}
