namespace CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure
{
    public interface IExpression : ISymbol
    {
        bool IsVisibleToEditor { get; }
    }
}
