using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using NUnit.Framework;
using Qik.LanguageEngine.IntegrationTests.Helpers;
using System;

namespace Qik.LanguageEngine.IntegrationTests
{
    [TestFixture]
    [Category("Qik.ScriptExamples")]
    [Category("Tests.Integration")]
    public class TestBench
    {
        [Test]
        public void ScriptExamples_InferPrimaryKeyFromPrimaryKeyOption_OutputsPrimaryKey()
        {
            string scriptText = TxtFile.ReadText("InferPK.txt");
            ICompiler compiler = new Compiler();
            compiler.Compile(scriptText);

            compiler.Input("@table", "MyTable");
            compiler.Input("@userPrimaryKey", "CustomMyTableId");

            string table = compiler.GetValueOfSymbol("@table");
            string userPrimaryKey = compiler.GetValueOfSymbol("@userPrimaryKey");

            // there is no default, so primaryKeyOption1 = null, inferredKeyOption1 should be null?
            string primaryKeyOption1 = compiler.GetValueOfSymbol("@primaryKeyOption");
            string inferredKeyOption1 = compiler.GetValueOfSymbol("@inferredPrimaryKey");

            // made a selection: inferredKeyOption2 expected to be @userPrimaryKey.
            compiler.Input("@primaryKeyOption", "CUSTOM");
            string primaryKeyOption2 = compiler.GetValueOfSymbol("@primaryKeyOption");
            string inferredKeyOption2 = compiler.GetValueOfSymbol("@inferredPrimaryKey");

            // made a selection: inferredKeyOption2 expected to be @table + "Id".
            compiler.Input("@primaryKeyOption", "INFERRED");
            string primaryKeyOption3 = compiler.GetValueOfSymbol("@primaryKeyOption");
            string inferredKeyOption3 = compiler.GetValueOfSymbol("@inferredPrimaryKey");

            // ensure both are null
            Assert.AreEqual(null, primaryKeyOption1);
            Assert.AreEqual(null, inferredKeyOption1);

            // ensure the custom primary key is used.
            Assert.AreEqual("CUSTOM", primaryKeyOption2);
            Assert.AreEqual("CustomMyTableId", inferredKeyOption2);

            // ensure the inferred primary key is used.
            Assert.AreEqual("INFERRED", primaryKeyOption3);
            Assert.AreEqual("MyTableId", inferredKeyOption3);
        }

        [Test]
        public void ScriptExamples_CreateMultilineScriptInExpression_OutputsCorrectly()
        {
            ICompiler compiler = new Compiler();
            compiler.Compile(TxtFile.ReadText("MultiLine.txt"));

            IGenerator generator = new Generator();
            string output = generator.Generate(compiler, TxtFile.ReadText("MultiLine.tpl"));

            Assert.AreEqual(TxtFile.ReadText("MultiLine.out"), output);
        }

        [Test]
        public void ScriptExamples_CreateStoredProcOutput_BuildsCorrectSymbolsAndOutputValues()
        {
            ICompiler compiler = new Compiler();
            compiler.Compile(TxtFile.ReadText("StoredProc.txt"));

            IExpression[] expressions = compiler.Expressions;
            IInputField[] inputFields = compiler.InputFields;

            Assert.IsTrue(expressions.Length > 0);
            Assert.IsTrue(inputFields.Length > 0);

            string authorName = compiler.GetValueOfSymbol("@authorName");
            string database = compiler.GetValueOfSymbol("@database");
            string todayDate = compiler.GetValueOfSymbol("@date");
            string authorCode = compiler.GetValueOfSymbol("@authorCode");
            string description = compiler.GetValueOfSymbol("@desc");
            string procTitle = compiler.GetValueOfSymbol("@name");

            compiler.Input("@name", "StoredProcName");
            compiler.Input("@database", "MSDF_DW");
            compiler.Input("@context", "BE");
            string procName = compiler.GetValueOfSymbol("@procName");
            string fileTitle = compiler.GetValueOfSymbol("@fileTitle");
            string filePath = compiler.GetValueOfSymbol("@filePath");
            string database2 = compiler.GetValueOfSymbol("@database");

            Assert.AreEqual("Rob Blake", authorName);
            Assert.AreEqual("MSDF_DM", database);
            Assert.AreEqual("0505c", authorCode);
            Assert.AreEqual(null, description);
            Assert.AreEqual(null, procTitle);
            Assert.AreEqual(DateTime.Now.ToString("dd/MM/yyyy"), todayDate);

            Assert.AreEqual("pRpt_StoredProcName", procName);
            Assert.AreEqual("pRpt_StoredProcName.sql", fileTitle);
            Assert.AreEqual(@"D:\Sandbox\MSDF\Code\SQLQueries\DataMartV2\Reports\pRpt_StoredProcName.sql", filePath);
            Assert.AreEqual("MSDF_DW", database2);
        }

        [Test]
        public void ScriptExamples_HtmlEncodeFunction_Encodes()
        {
            ICompiler compiler = new Compiler();
            compiler.Compile(TxtFile.ReadText("HtmlEncode.txt"));
            compiler.Input("@normalText", @"Hello 'World'");

            string encodedText = compiler.GetValueOfSymbol("@encodedText");
            string decodedText = compiler.GetValueOfSymbol("@decodedText");

            Assert.AreEqual(@"Hello &#39;World&#39;", encodedText);
            Assert.AreEqual(@"Hello 'World'", decodedText);
        }

        [Test]
        public void ScriptExamples_BatchCompileFields_GeneratesBatchOutput()
        {
            IBatchCompiler batchCompiler = new BatchCompiler();
            batchCompiler.Compile(TxtFile.ReadText("Batch.tpl"));

            // this must take place afterwards, otherwise it will be cleared by the compile.
            batchCompiler.CreateFieldInput("@Column1", "Title", "Description");
            batchCompiler.CreateFieldInput("@Column2", "Title", "Description");
            batchCompiler.Input("@Column1", "COL 1");
            batchCompiler.Input("@Column2", "COL 2");

            ISymbolInfo[] symbolInfo = batchCompiler.GetSymbolInfoSet(new string[] { "@Column1", "@Column2" });

            Generator generator = new Generator();
            string output = generator.Generate(batchCompiler, "@{combination}");

            Assert.AreEqual("COL 1 - COL 2", output);

            Assert.AreEqual(2, symbolInfo.Length);
        }


        [Test]
        public void ScriptExamples_OptionInput_Parsed_IsNotAPlaceholder()
        {
            string scriptText = TxtFile.ReadText("OptionBox.txt");
            ICompiler compiler = new Compiler();
            compiler.Compile(scriptText);

            IInputField inputField = compiler.InputFields[0];
            Assert.IsFalse(inputField.IsPlaceholder);
        }
    }
}
