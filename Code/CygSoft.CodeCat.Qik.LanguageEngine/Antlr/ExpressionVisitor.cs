using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using System;
using System.Collections.Generic;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Antlr
{
    internal class ExpressionVisitor : QikTemplateBaseVisitor<BaseFunction>
    {
        private GlobalTable scopeTable;
        private IErrorReport errorReport;

        internal ExpressionVisitor(GlobalTable scopeTable, IErrorReport errorReport)
        {
            this.scopeTable = scopeTable;
            this.errorReport = errorReport;
        }

        public override BaseFunction VisitExprDecl(QikTemplateParser.ExprDeclContext context)
        {
            string id = context.VARIABLE().GetText();

            SymbolArguments symbolArguments = new SymbolArguments(errorReport);
            symbolArguments.Process(context.declArgs());

            if (context.concatExpr() != null)
            {
                ConcatenateFunction concatenateFunc = GetConcatenateFunction(context.concatExpr());
                ExpressionSymbol expression =
                    new ExpressionSymbol(errorReport, id, symbolArguments.Title, symbolArguments.Description,
                        symbolArguments.IsPlaceholder, symbolArguments.IsVisibleToEditor, concatenateFunc);
                scopeTable.AddSymbol(expression);
            }
            else if (context.optExpr() != null)
            {
                var expr = context.optExpr();
                BaseFunction ifFunc = VisitOptExpr(context.optExpr());

                ExpressionSymbol expression = new ExpressionSymbol(errorReport, id, symbolArguments.Title, symbolArguments.Description,
                    symbolArguments.IsPlaceholder, symbolArguments.IsVisibleToEditor, ifFunc);
                scopeTable.AddSymbol(expression);
            }
            else if (context.expr() != null)
            {
                BaseFunction function = VisitExpr(context.expr());
                ExpressionSymbol expression = new ExpressionSymbol(errorReport, id, symbolArguments.Title, symbolArguments.Description,
                    symbolArguments.IsPlaceholder, symbolArguments.IsVisibleToEditor, function);
                scopeTable.AddSymbol(expression);
            }

            return null;
        }

        public override BaseFunction VisitOptExpr(QikTemplateParser.OptExprContext context)
        {
            int line = context.Start.Line;
            int column = context.Start.Column;

            string id = context.VARIABLE().GetText();
            IfDecissionFunction ifFunc = new IfDecissionFunction(new FuncInfo("Float", line, column), this.scopeTable, id);

            foreach (var ifOptContext in context.ifOptExpr())
            {
                string text = ifOptContext.STRING().GetText();

                if (ifOptContext.concatExpr() != null)
                {
                    ConcatenateFunction concatenateFunc = GetConcatenateFunction(ifOptContext.concatExpr());
                    ifFunc.AddFunction(text, concatenateFunc);
                }
                else if (ifOptContext.expr() != null)
                {
                    BaseFunction function = VisitExpr(ifOptContext.expr());
                    ifFunc.AddFunction(text, function);
                }
            }

            return ifFunc;
        }

        public override BaseFunction VisitFunc(QikTemplateParser.FuncContext context)
        {
            BaseFunction func = null;

            if (context.IDENTIFIER() != null)
            {
                string funcIdentifier = context.IDENTIFIER().GetText();
                List<BaseFunction> functionArguments = CreateArguments(context.funcArg());
                FuncInfo funcInfo = new FuncInfo(funcIdentifier, context.Start.Line, context.Start.Column);

                switch (funcIdentifier)
                {
                    case "camelCase":
                        CamelCaseFunction camelCaseFunc = new CamelCaseFunction(funcInfo, scopeTable, functionArguments);
                        func = camelCaseFunc;
                        break;
                    case "currentDate":
                        CurrentDateFunction currentDateFunc = new CurrentDateFunction(funcInfo, scopeTable, functionArguments);
                        func = currentDateFunc;
                        break;
                    case "lowerCase":
                        LowerCaseFunction lowerCaseFunc = new LowerCaseFunction(funcInfo, scopeTable, functionArguments);
                        func = lowerCaseFunc;
                        break;
                    case "upperCase":
                        UpperCaseFunction upperCaseFunc = new UpperCaseFunction(funcInfo, scopeTable, functionArguments);
                        func = upperCaseFunc;
                        break;
                    case "properCase":
                        ProperCaseFunction properCaseFunc = new ProperCaseFunction(funcInfo, scopeTable, functionArguments);
                        func = properCaseFunc;
                        break;
                    case "removeSpaces":
                        RemoveSpacesFunction removeSpacesFunc = new RemoveSpacesFunction(funcInfo, scopeTable, functionArguments);
                        func = removeSpacesFunc;
                        break;
                    case "removePunctuation":
                        RemovePunctuationFunction removePunctuationFunc = new RemovePunctuationFunction(funcInfo, scopeTable, functionArguments);
                        func = removePunctuationFunc;
                        break;
                    case "replace":
                        ReplaceFunction replaceFunc = new ReplaceFunction(funcInfo, scopeTable, functionArguments);
                        func = replaceFunc;
                        break;
                    case "indentLine":
                        IndentFunction indentFunc = new IndentFunction(funcInfo, scopeTable, functionArguments);
                        func = indentFunc;
                        break;
                    case "doubleQuotes": // for backward compatibility...
                        DoubleQuoteFunction doubleQuoteFunction = new DoubleQuoteFunction(funcInfo, scopeTable, functionArguments);
                        func = doubleQuoteFunction;
                        break;
                    case "doubleQuote":
                        DoubleQuoteFunction doubleQuoteFunction_Ex = new DoubleQuoteFunction(funcInfo, scopeTable, functionArguments);
                        func = doubleQuoteFunction_Ex;
                        break;
                    case "htmlEncode":
                        HtmlEncodeFunction htmlEncodeFunction = new HtmlEncodeFunction(funcInfo, scopeTable, functionArguments);
                        func = htmlEncodeFunction;
                        break;

                    case "htmlDecode":
                        HtmlDecodeFunction htmlDecodeFunction = new HtmlDecodeFunction(funcInfo, scopeTable, functionArguments);
                        func = htmlDecodeFunction;
                        break;
                    case "guid":
                        GuidFunction guidFunction = new GuidFunction(funcInfo, scopeTable, functionArguments);
                        func = guidFunction;
                        break;
                    case "padLeft":
                        PadLeftFunction padLeftFunction = new PadLeftFunction(funcInfo, scopeTable, functionArguments);
                        func = padLeftFunction;
                        break;
                    case "padRight":
                        PadRightFunction padRightFunction = new PadRightFunction(funcInfo, scopeTable, functionArguments);
                        func = padRightFunction;
                        break;

                    default:
                        throw new NotSupportedException(string.Format("Function \"{0}\" is not supported in this context.", funcIdentifier));
                }
            }
            return func;
        }

        public override BaseFunction VisitExpr(QikTemplateParser.ExprContext context)
        {
            int line = context.Start.Line;
            int column = context.Start.Column;

            if (context.STRING() != null)
            {
                FuncInfo funcInfo = new FuncInfo("String", line, column);
                return new TextFunction(funcInfo, scopeTable, Common.StripOuterQuotes(context.STRING().GetText()));
            }

            else if (context.VARIABLE() != null)
            {
                FuncInfo funcInfo = new FuncInfo("Variable", line, column);
                return new VariableFunction(funcInfo, scopeTable, context.VARIABLE().GetText());
            }

            else if (context.CONST() != null)
            {
                string constantText = context.CONST().GetText();
                if (constantText == "NEWLINE")
                    return new NewlineFunction(new FuncInfo("Constant", line, column));
                else
                    return new ConstantFunction(new FuncInfo("Constant", line, column), scopeTable, context.CONST().GetText());
            }

            else if (context.INT() != null)
                return new IntegerFunction(new FuncInfo("Int", line, column), scopeTable, context.INT().GetText());

            else if (context.FLOAT() != null)
                return new FloatFunction(new FuncInfo("Float", line, column), scopeTable, context.FLOAT().GetText());

            // recurse...
            else if (context.func() != null)
                return VisitFunc(context.func());

            else
                return null;
        }

        private List<BaseFunction> CreateArguments(IReadOnlyList<QikTemplateParser.FuncArgContext> funcArgs)
        {
            List<BaseFunction> functionArguments = new List<BaseFunction>();

            foreach (QikTemplateParser.FuncArgContext funcArg in funcArgs)
            {
                QikTemplateParser.ConcatExprContext concatExpr = funcArg.concatExpr();
                QikTemplateParser.ExprContext expr = funcArg.expr();

                if (concatExpr != null)
                {
                    ConcatenateFunction concatenateFunc = GetConcatenateFunction(concatExpr);
                    functionArguments.Add(concatenateFunc);
                }
                else if (expr != null)
                {
                    BaseFunction function = VisitExpr(expr);
                    functionArguments.Add(function);
                }
            }
            return functionArguments;
        }

        private ConcatenateFunction GetConcatenateFunction(QikTemplateParser.ConcatExprContext context)
        {
            int line = context.Start.Line;
            int column = context.Start.Column;

            ConcatenateFunction concatenateFunc = new ConcatenateFunction(new FuncInfo("Concatenation", line, column), this.scopeTable);

            IReadOnlyList<QikTemplateParser.ExprContext> expressions = context.expr();

            foreach (QikTemplateParser.ExprContext expr in expressions)
            {
                BaseFunction result = VisitExpr(expr);
                concatenateFunc.AddFunction(result);
            }

            return concatenateFunc;
        }
    }
}
