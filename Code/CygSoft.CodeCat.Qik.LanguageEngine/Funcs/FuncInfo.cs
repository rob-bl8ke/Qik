namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class FuncInfo
    {
        public int Line { get; private set; }
        public int Column { get; private set; }
        public string Name { get; private set; }

        public FuncInfo(string name, int line, int column)
        {
            this.Line = line;
            this.Column = column;
            this.Name = name;
        }
    }
}
