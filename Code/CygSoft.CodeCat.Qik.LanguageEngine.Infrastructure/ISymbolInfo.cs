namespace CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure
{
    public interface ISymbolInfo
    {
        string Description { get; }
        string Placeholder { get; }
        string Symbol { get; }
        string Title { get; }
    }
}
