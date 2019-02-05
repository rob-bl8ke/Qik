using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;

namespace CygSoft.CodeCat.Qik.LanguageEngine
{
    public class Generator : IGenerator
    {
        public string Generate(IBatchCompiler batchCompiler, string templateText)
        {
            string input = templateText;

            foreach (string placeholder in batchCompiler.Placeholders)
            {
                string output = batchCompiler.GetValueOfPlaceholder(placeholder);
                input = input.Replace(placeholder, output);
            }

            return input;
        }

        public string Generate(ICompiler compiler, string templateText)
        {
            string input = templateText;

            foreach (string placeholder in compiler.Placeholders)
            {
                string output = compiler.GetValueOfPlaceholder(placeholder);
                input = input.Replace(placeholder, output);
            }

            return input;
        }
    }
}
