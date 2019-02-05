using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Scope
{
    internal class GlobalTable
    {
        private class SymbolInfo : ISymbolInfo
        {
            public string Symbol { get; private set; }
            public string Placeholder { get; private set; }
            public string Title { get; private set; }
            public string Description { get; private set; }

            public SymbolInfo(string placeholder, string symbol, string title, string description)
            {
                this.Symbol = symbol;
                this.Placeholder = placeholder;
                this.Title = title;
                this.Description = description;
            }
        }

        private Dictionary<string, BaseSymbol> table = new Dictionary<string, BaseSymbol>();

        public string[] Symbols
        {
            get
            {
                if (table.Keys.Select(r => r).Any())
                    return table.Keys.Select(r => r).ToArray();
                else
                    return new string[0];
            }
        }

        public string[] Placeholders
        {
            get
            {
                if (table.Values.Where(r => r.IsPlaceholder == true).Any())
                    return table.Values.Where(r => r.IsPlaceholder == true).Select(r => r.Placeholder).ToArray();
                else
                    return new string[0];
            }
        }

        public IInputField[] InputFields { get { return this.table.Values.OfType<IInputField>().ToArray(); } }
        public IExpression[] Expressions { get { return this.table.Values.OfType<IExpression>().ToArray(); } }

        internal void Clear()
        {
            table.Clear();
        }

        internal void AddSymbol(BaseSymbol symbol)
        {
            if (!table.ContainsKey(symbol.Symbol))
            {
                table.Add(symbol.Symbol, symbol);
            }
        }

        internal void Input(string inputSymbol, string value)
        {
            if (table.ContainsKey(inputSymbol))
            {
                BaseSymbol symbol = table[inputSymbol];

                if (symbol is AutoInputSymbol)
                {
                    AutoInputSymbol autoInputSymbol = table[inputSymbol] as AutoInputSymbol;
                    autoInputSymbol.SetValue(value);
                }
                else if (symbol is TextInputSymbol)
                {
                    TextInputSymbol textInputSymbol = table[inputSymbol] as TextInputSymbol;
                    textInputSymbol.SetValue(value);
                }
                else if (symbol is OptionInputSymbol)
                {
                    OptionInputSymbol optionInputSymbol = table[inputSymbol] as OptionInputSymbol;
                    optionInputSymbol.SelectOption(value);
                }
            }
        }

        public ISymbolInfo GetPlaceholderInfo(string placeholder)
        {
            List<BaseSymbol> symbols = table.Values.Cast<BaseSymbol>().ToList();

            if (symbols.Any(s => s.Placeholder == placeholder))
            {
                BaseSymbol baseSymbol = symbols.Where(s => s.Placeholder == placeholder).SingleOrDefault() as BaseSymbol;
                ISymbolInfo placeholderInfo =
                    new SymbolInfo(baseSymbol.Placeholder, baseSymbol.Symbol, baseSymbol.Title, baseSymbol.Description);
                return placeholderInfo;
            }

            return null;
        }

        public ISymbolInfo GetSymbolInfo(string symbol)
        {
            if (table.ContainsKey(symbol))
            {
                BaseSymbol baseSymbol = table[symbol];
                ISymbolInfo symbolInfo =
                    new SymbolInfo(baseSymbol.Placeholder, baseSymbol.Symbol, baseSymbol.Title, baseSymbol.Description);
                return symbolInfo;
            }
            return null;
        }

        public ISymbolInfo[] GetSymbolInfoSet(string[] symbols)
        {
            List<ISymbolInfo> symbolInfoSet = new List<ISymbolInfo>();
            foreach (string symbol in symbols)
            {
                ISymbolInfo symbolInfo = GetSymbolInfo(symbol);
                if (symbolInfo != null)
                    symbolInfoSet.Add(symbolInfo);
            }
            return symbolInfoSet.ToArray();
        }

        public string GetValueOfSymbol(string symbol)
        {
            if (table.ContainsKey(symbol))
            {
                BaseSymbol baseSymbol = table[symbol];
                return baseSymbol.Value;
            }
            return null;
        }

        public string GetValueOfPlacholder(string placeholder)
        {
            List<BaseSymbol> symbols = table.Values.Cast<BaseSymbol>().ToList();

            if (symbols.Any(s => s.Placeholder == placeholder))
            {
                BaseSymbol baseSymbol = symbols.Where(s => s.Placeholder == placeholder).SingleOrDefault() as BaseSymbol;
                return baseSymbol.Value;
            }

            return null;
        }

        public string GetTitleOfPlacholder(string placeholder)
        {
            List<BaseSymbol> symbols = table.Values.Cast<BaseSymbol>().ToList();

            if (symbols.Any(s => s.Placeholder == placeholder))
            {
                BaseSymbol baseSymbol = symbols.Where(s => s.Placeholder == placeholder).SingleOrDefault() as BaseSymbol;
                return baseSymbol.Title;
            }

            return null;
        }
    }
}
