using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using NUnit.Framework;

namespace LanguageEngine.Tests.UnitTests.Functions
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Functions")]
    [Category("Tests.UnitTests")]
    class TextFunctionTests
    {
        [Test]
        public void BasicTextFunction_InputText_OutputsText()
        {
            // BEFORE REMOVING THIS TEST METHOD YOU NEED TO WRITE TESTS FOR ALL ITS POSSIBILITIES IN THE NEW STYLE BELOW

            GlobalTable globalTable = new GlobalTable();

            string literalText = "Rob Blake";
            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@authorName", "Author Name", "Description", true, true, new TextFunction(new FuncInfo("stub", 1, 1), globalTable, literalText));
            Assert.AreEqual("@authorName", expressionSymbol.Symbol);
            Assert.AreEqual("@{authorName}", expressionSymbol.Placeholder);
            Assert.AreEqual("Author Name", expressionSymbol.Title);

            Assert.AreEqual("Rob Blake", expressionSymbol.Value);
        }
    }
}
