using System.Collections.Immutable;
using Youslyn.CodeAnalysis.Diagnostics;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal sealed class Parser
    {
        private readonly DiagnosticBag _diagnosticBag;
        private readonly ImmutableArray<SyntaxToken> _tokens;
        private int _position;

        public Parser(string text)
        {
            ImmutableArray<SyntaxToken>.Builder? tokensBuilder = ImmutableArray.CreateBuilder<SyntaxToken>();
            var lexer = new Lexer(text);

            while (true)
            {
                SyntaxToken token = lexer.EatToken();
                if (token.Kind != SyntaxKind.WhitespaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokensBuilder.Add(token);
                }

                if (token.Kind == SyntaxKind.EndOfFileToken)
                {
                    break;
                }
            }

            _tokens = tokensBuilder.ToImmutable();
            _diagnosticBag = lexer.DiagnosticBag;
        }

        public ImmutableArray<Diagnostic> Diagnostics => _diagnosticBag.Diagnostics;

        private SyntaxToken Current => _tokens[_position];

        public SyntaxTree Parse()
        {
            var root = ParseExpression();
            _ = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(root, Diagnostics);
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
            ExpressionSyntax left;

            var unaryPrecedence = SyntaxFacts.GetUnaryPrecedence(Current);
            if (unaryPrecedence != 0 && unaryPrecedence >= parentPrecedence)
            {
                var operatorToken = NextToken();
                var operand = ParseExpression(unaryPrecedence);
                left = new UnaryExpressionSyntax(operatorToken, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
            }

            while (true)
            {
                var precedence = SyntaxFacts.GetBinaryPrecedence(Current);
                if (precedence == 0 || precedence <= parentPrecedence)
                {
                    break;
                }

                var operatorToken = NextToken();
                var right = ParseExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
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
                var expression = ParseExpression();
                var close = Match(SyntaxKind.CloseParenToken);
                return new ParenthesizedExpressionSyntax(open, expression, close);
            }

            var number = Match(SyntaxKind.NumericLiteralToken);
            return new NumericExpressionSyntax(number);
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private void AddError(string message)
        {
            _diagnosticBag.AddDiagnostic(message, DiagnosticSeverity.Error);
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
