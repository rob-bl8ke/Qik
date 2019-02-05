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
    public class PadFunctionTests
    {
        [Test]
        public void PadLeftFunction_Pads_Correctly()
        {
            string funcText = $"padLeft(\"12\", \"0\", 5)";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("00012", output);
        }

        [Test]
        public void PadRightFunction_Pads_Correctly()
        {
            string funcText = $"padRight(\"12\", \"0\", 5)";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual("12000", output);
        }
    }
}
