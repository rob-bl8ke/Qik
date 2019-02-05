using LanguageEngine.Tests.UnitTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qik.LanguageEngine.UnitTests.Functions
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Functions")]
    [Category("Tests.UnitTests")]
    public class GuidFunctionTests
    {
        [Test]
        public void GuidFunction_Returns_NewGuid()
        {
            string funcText = $"guid(\"u\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Guid guid = new Guid(output);
        }

        [Test]
        public void GuidFunction_Returns_UpperCase_Guid_When_UpperCase_Specified()
        {
            string funcText = $"guid(\"u\")";
            string original = TestHelpers.EvaluateCompilerFunction(funcText);
            string ucased = original.ToUpper();

            Assert.AreEqual(original, ucased);
        }

        [Test]
        public void GuidFunction_Returns_LowerCase_Guid_When_LowerCase_Specified()
        {
            string funcText = $"guid(\"l\")";
            string original = TestHelpers.EvaluateCompilerFunction(funcText);
            string lcased = original.ToLower();

            Assert.AreEqual(original, lcased);
        }

        [Test]
        public void GuidFunction_Returns_LowerCase_Guid_When_Nothing_Specified()
        {
            string funcText = $"guid(\"\")";
            string original = TestHelpers.EvaluateCompilerFunction(funcText);
            string lcased = original.ToLower();

            Assert.AreEqual(original, lcased);
        }
    }
}
