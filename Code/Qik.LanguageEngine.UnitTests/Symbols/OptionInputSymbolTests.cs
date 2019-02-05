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
    public class OptionInputSymbolTests
    {
        [Test]
        public void OptionInputSymbol_Cast_CanCastToIOptionsFieldInterface()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            Assert.That(optionInputSymbol is IOptionsField);
        }

        [Test]
        public void OptionInputSymbol_Cast_CanCastToInputFieldInterface()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            Assert.That(optionInputSymbol is IInputField);
        }

        [Test]
        public void OptionInputSymbol_Initialized_WithoutDefaultValue_DefaultValueIsNull()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface();

            Assert.AreEqual(null, optionsField.DefaultValue);
            Assert.AreEqual(null, optionInputSymbol.DefaultValue);
            Assert.AreEqual(null, optionsField.Value);
            Assert.AreEqual(null, optionInputSymbol.Value);
        }

        [Test]
        public void OptionInputSymbol_Initialized_WithoutInitialSelection_SelectionIsNull()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface();

            Assert.AreEqual(null, optionsField.SelectedIndex);
            Assert.AreEqual(null, optionInputSymbol.SelectedIndex);
        }

        [Test]
        public void OptionInputSymbol_Initialized_InitializesSymbol()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface();

            Assert.AreEqual("@databaseOptions", optionInputSymbol.Symbol);
            Assert.AreEqual("@databaseOptions", optionsField.Symbol);
        }

        [Test]
        public void OptionInputSymbol_Initialized_InitializesPlaceholder()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface();

            Assert.AreEqual("@{databaseOptions}", optionInputSymbol.Placeholder);
            Assert.AreEqual("@{databaseOptions}", optionsField.Placeholder);
        }

        [Test]
        public void OptionInputSymbol_Initialized_InitializesOptionTitles()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface();

            Assert.AreEqual("Adventure Works Database", optionsField.OptionTitle("ADVWORKS"));
            Assert.AreEqual("Published Books Database", optionsField.OptionTitle("PUBBOOKS"));
            Assert.AreEqual("Adventure Works Database", optionInputSymbol.OptionTitle("ADVWORKS"));
            Assert.AreEqual("Published Books Database", optionInputSymbol.OptionTitle("PUBBOOKS"));
        }

        [Test]
        public void OptionInputSymbol_Initialized_WithDefaultValue_DefaultValueIsCorrect()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions(defaultValue: "ADVWORKS");
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface(defaultValue: "ADVWORKS");

            Assert.AreEqual("ADVWORKS", optionsField.DefaultValue);
            Assert.AreEqual("ADVWORKS", optionInputSymbol.DefaultValue);
            Assert.AreEqual(0, optionsField.SelectedIndex);
            Assert.AreEqual(0, optionInputSymbol.SelectedIndex);
            Assert.AreEqual("ADVWORKS", optionsField.Value);
            Assert.AreEqual("ADVWORKS", optionInputSymbol.Value);
        }

        [Test]
        public void OptionInputSymbol_Initialized_WithInitialSelection_SelectionIsSelected()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions();
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface();

            Assert.AreEqual(null, optionsField.SelectedIndex);
            Assert.AreEqual(null, optionInputSymbol.SelectedIndex);
        }

        [Test]
        public void OptionInputSymbol_MakeSelectionsByIndex_ReturnsCorrectOption()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions(defaultValue: "ADVWORKS");

            optionInputSymbol.SelectOption("0");
            string indexOption_at_0 = optionInputSymbol.Value;

            optionInputSymbol.SelectOption("1");
            string indexOption_at_1 = optionInputSymbol.Value;

            optionInputSymbol.SelectOption(0);
            string indexOption_index_at_0 = optionInputSymbol.Value;

            optionInputSymbol.SelectOption("ADVWORKS");
            string valueOption_at_0 = optionInputSymbol.Value;

            optionInputSymbol.SelectOption("PUBBOOKS");
            string valueOption_at_1 = optionInputSymbol.Value;

            Assert.AreEqual("ADVWORKS", indexOption_at_0);
            Assert.AreEqual("PUBBOOKS", indexOption_at_1);
            Assert.AreEqual("ADVWORKS", valueOption_at_0);
            Assert.AreEqual("PUBBOOKS", valueOption_at_1);
            Assert.AreEqual("ADVWORKS", indexOption_index_at_0);
        }

        [Test]
        public void OptionInputSymbol_MakeSelectionsByIndexOnInterface_ReturnsCorrectOption()
        {
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface(defaultValue: "ADVWORKS");

            optionsField.SelectOption("0");
            string indexOption_at_0 = optionsField.Value;

            optionsField.SelectOption("1");
            string indexOption_at_1 = optionsField.Value;

            optionsField.SelectOption(0);
            string indexOption_index_at_0 = optionsField.Value;

            optionsField.SelectOption("ADVWORKS");
            string valueOption_at_0 = optionsField.Value;

            optionsField.SelectOption("PUBBOOKS");
            string valueOption_at_1 = optionsField.Value;

            Assert.AreEqual("ADVWORKS", indexOption_at_0);
            Assert.AreEqual("PUBBOOKS", indexOption_at_1);
            Assert.AreEqual("ADVWORKS", valueOption_at_0);
            Assert.AreEqual("PUBBOOKS", valueOption_at_1);
            Assert.AreEqual("ADVWORKS", indexOption_index_at_0);
        }

        [Test]
        public void OptionInputSymbol_GetSelectedOptionTitles()
        {
            OptionInputSymbol optionInputSymbol = TestHelpers.CreateOptionInputSymbol_DatabaseOptions(defaultValue: "ADVWORKS");

            string indexOption_at_0 = optionInputSymbol.OptionTitle("0");
            string indexOption_at_1 = optionInputSymbol.OptionTitle("1");
            string valueOption_at_0 = optionInputSymbol.OptionTitle("ADVWORKS");
            string valueOption_at_1 = optionInputSymbol.OptionTitle("PUBBOOKS");

            string optionIndex_at_0 = optionInputSymbol.OptionTitle(0);
            string optionIndex_at_1 = optionInputSymbol.OptionTitle(1);

            Assert.AreEqual("Adventure Works Database", indexOption_at_0);
            Assert.AreEqual("Published Books Database", indexOption_at_1);
            Assert.AreEqual("Adventure Works Database", valueOption_at_0);
            Assert.AreEqual("Published Books Database", valueOption_at_1);

            Assert.AreEqual("Adventure Works Database", optionIndex_at_0);
            Assert.AreEqual("Published Books Database", optionIndex_at_1);
        }

        [Test]
        public void OptionInputSymbol_GetOptionTitles_Interface()
        {
            IOptionsField optionsField = TestHelpers.CreateOptionInputSymbol_DatabaseOptions_AsInterface(defaultValue: "ADVWORKS");

            string indexOption_at_0 = optionsField.OptionTitle("0");
            string indexOption_at_1 = optionsField.OptionTitle("1");
            string valueOption_at_0 = optionsField.OptionTitle("ADVWORKS");
            string valueOption_at_1 = optionsField.OptionTitle("PUBBOOKS");

            string optionIndex_at_0 = optionsField.OptionTitle(0);
            string optionIndex_at_1 = optionsField.OptionTitle(1);

            Assert.AreEqual("Adventure Works Database", indexOption_at_0);
            Assert.AreEqual("Published Books Database", indexOption_at_1);
            Assert.AreEqual("Adventure Works Database", valueOption_at_0);
            Assert.AreEqual("Published Books Database", valueOption_at_1);

            Assert.AreEqual("Adventure Works Database", optionIndex_at_0);
            Assert.AreEqual("Published Books Database", optionIndex_at_1);
        }
    }
}
