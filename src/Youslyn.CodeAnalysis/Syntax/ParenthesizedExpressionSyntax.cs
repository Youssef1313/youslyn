using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        private SyntaxToken _openParen;
        private ExpressionSyntax _expression;
        private SyntaxToken _closeParen;

        public ParenthesizedExpressionSyntax(SyntaxToken openParen, ExpressionSyntax expression, SyntaxToken closeParen)
        {
            _openParen = openParen;
            _expression = expression;
            _closeParen = closeParen;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create(
            new SyntaxNodeOrToken(_openParen), new SyntaxNodeOrToken(_expression), new SyntaxNodeOrToken(_closeParen));
    }
}
