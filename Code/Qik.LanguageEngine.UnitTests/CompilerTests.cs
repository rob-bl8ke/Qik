using CygSoft.CodeCat.Qik.LanguageEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qik.LanguageEngine.UnitTests
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Compiler")]
    [Category("Tests.UnitTests")]
    class CompilerTests
    {
        [Test]
        public void Compiler_WhenCompiled_WithIncorrectCase_Title_SyntaxError_Fires_SyntaxErrorDetected_Event()
        {
            bool wasCalled = false;
            Compiler compiler = new Compiler();
            compiler.CompileError += (s, e) => wasCalled = true;

            compiler.Compile("@dataType = text[title=\"5.Field Datatype)\", Description=\"The datatype for the field (column).\"];");

            Assert.IsTrue(wasCalled, "Expect that CompileError event is fired when a syntax error is discovered.");
        }

        [Test]
        public void Compiler_WhenCompiled_WithIncorrectCase_Description_SyntaxError_Fires_SyntaxErrorDetected_Event()
        {
            bool wasCalled = false;
            Compiler compiler = new Compiler();
            compiler.CompileError += (s, e) =>
            {
                wasCalled = true;
            };

            compiler.Compile("@dataType = Text[title=\"5.Field Datatype)\", description=\"The datatype for the field (column).\"];");

            Assert.IsTrue(wasCalled, "Expect that CompileError event is fired when a syntax error is discovered.");
        }
    }
}
