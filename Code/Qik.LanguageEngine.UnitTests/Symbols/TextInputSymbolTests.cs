using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using LanguageEngine.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace LanguageEngine.Tests.UnitTests.Symbols
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Symbol")]
    [Category("Tests.UnitTests")]
    public class TextInputSymbolTests
    {
        [Test]
        public void TextInputSymbol_Cast_CanCastToIInputFieldInterface()
        {
            TextInputSymbol textInputSymbol = TestHelpers.CreateTextInputSymbol_Author();
            Assert.That(textInputSymbol is IInputField);
        }

        [Test]
        public void TextInputSymbol_Initialize_InitializesPlaceholder()
        {
            TextInputSymbol textInputSymbol = TestHelpers.CreateTextInputSymbol_Author(description: "Description");
            Assert.AreEqual("@{authorName}", textInputSymbol.Placeholder);
        }

        [Test]
        public void TextInputSymbol_Initialize_InitializesSymbol()
        {
            TextInputSymbol textInputSymbol = TestHelpers.CreateTextInputSymbol_Author(description: "Description");

            Assert.AreEqual("@authorName", textInputSymbol.Symbol);
            Assert.AreEqual("Author Name", textInputSymbol.Title);
        }

        [Test]
        public void TextInputSymbol_Initialized_WithDefaultValue_DefaultValueIsCorrect()
        {
            TextInputSymbol textInputSymbol = TestHelpers.CreateTextInputSymbol_Author(description: "Description", defaultValue: "Rob Blake");
            Assert.AreEqual("Rob Blake", textInputSymbol.DefaultValue);
            Assert.AreEqual("Rob Blake", textInputSymbol.Value);
        }

        [Test]
        public void TextInputSymbol_Initialized_WithoutDefaultValue_DefaultValueIsNull()
        {
            TextInputSymbol textInputSymbol = TestHelpers.CreateTextInputSymbol_Author(description: "Description");
            Assert.AreEqual(null, textInputSymbol.DefaultValue);
            Assert.AreEqual(null, textInputSymbol.Value);
        }


        [Test]
        public void TextInputSymbol_Initialized_WithDefaultValueButChanged_ValueIsCorrect()
        {
            TextInputSymbol textInputSymbol = TestHelpers.CreateTextInputSymbol_Author(description: "Description", defaultValue:"Rob Blake");

            textInputSymbol.SetValue("Andrew Botha");
            Assert.AreEqual("Andrew Botha", textInputSymbol.Value);
            Assert.AreEqual("Rob Blake", textInputSymbol.DefaultValue);
        }
    }
}
