using System.Collections.Immutable;
using Youslyn.CodeAnalysis.Diagnostic;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal sealed class Parser
    {
        private readonly ImmutableArray<DiagnosticDescriptor>.Builder _diagnosticsBuilder;
        private readonly ImmutableArray<SyntaxToken> _tokens;
        private int _position;

        public Parser(string text)
        {
            ImmutableArray<SyntaxToken>.Builder? tokensBuilder = ImmutableArray.CreateBuilder<SyntaxToken>();
            var lexer = new Lexer(text);

            while (true)
            {
                SyntaxToken token = lexer.EatToken();
                tokensBuilder.Add(token);
                if (token.Kind == SyntaxKind.EndOfFileToken)
                {
                    break;
                }
            }

            _tokens = tokensBuilder.ToImmutable();
            _diagnosticsBuilder = lexer.Diagnostics.ToBuilder();
        }

        public ImmutableArray<DiagnosticDescriptor> Diagnostics => _diagnosticsBuilder.ToImmutable();

        private SyntaxToken Current => _tokens[_position];

        public SyntaxTree Parse()
        {
            var root = ParseTerm();
            _ = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(root, Diagnostics);
        }

        /*
         * - Primary expression: the simplest kind of an expression and is the building block of more complex
         *   expressions. It can be a literal or a parenthesized expression.
         */

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParenToken)
            {
                var open = NextToken();
                var expression = ParseTerm();
                var close = Match(SyntaxKind.CloseParenToken);
                return new ParenthesizedExpressionSyntax(open, expression, close);
            }

            var number = Match(SyntaxKind.NumericLiteralToken);
            return new NumericExpressionSyntax(number);
        }

        private ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();
            while (Current.Kind == SyntaxKind.PlusToken ||
                Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();
            while (Current.Kind == SyntaxKind.AsteriskToken ||
                Current.Kind == SyntaxKind.SlashToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private void AddError(string message)
        {
            _diagnosticsBuilder.Add(new DiagnosticDescriptor(message, DiagnosticSeverity.Error));
        }

        /// <summary>
        /// If the current token matches <paramref name="kind"/>, returns it and advances the position.
        /// Otherwise, add a diagnostic and fabricates a token with the requested <paramref name="kind"/>.
        /// </summary>
        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
            {
                return NextToken();
            }

            AddError($"Expected token of kind '{kind}'. Found '{Current.Kind}'.");
            return new SyntaxToken(kind, null, null);

        }
    }
}
