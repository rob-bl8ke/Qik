using System;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure
{
    public class CompileErrorEventArgs
    {
        public int Line { get; private set; }
        public int Column { get; private set; }
        public string Message { get; private set; }
        public string OffendingSymbol { get; private set; }
        public string Location { get; private set; }

        public CompileErrorEventArgs(string location, int line, int column, string offendingSymbol, string message)
        {
            this.Line = line;
            this.Column = column;
            this.Message = message;
            this.OffendingSymbol = offendingSymbol;
            this.Location = location;
        }

        public CompileErrorEventArgs(Exception exception)
        {
            this.Line = 0;
            this.Column = 0;
            this.Message = exception.Message;
            this.OffendingSymbol = "";
            this.Location = "Main Template";
        }
    }
}
