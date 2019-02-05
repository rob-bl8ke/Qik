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
    public class RemovePunctuationTests
    {
        [Test]
        public void RemovePunctuationFunction_InputPunctuatedText_OutputPunctuationRemoved_1()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW
            GlobalTable globalTable = new GlobalTable();

            List<BaseFunction> functionArguments = new List<BaseFunction>();
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "LITERAL?!..TEXT."));

            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@removePunctuation", "Remove Punctuation Function", "Remove Punctuation Function", true, true, new RemovePunctuationFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments));
            Assert.AreEqual("@removePunctuation", expressionSymbol.Symbol);
            Assert.AreEqual("@{removePunctuation}", expressionSymbol.Placeholder);
            Assert.AreEqual("Remove Punctuation Function", expressionSymbol.Title);

            Assert.AreEqual("LITERALTEXT", expressionSymbol.Value);
        }

        [Test]
        public void RemovePunctuationFunction_InputPunctuatedText_OutputPunctuationRemoved()
        {
            string funcText = $"removePunctuation(\"LITERAL?!..TEXT.\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("LITERALTEXT", output);
        }
    }
}
