using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openParen, ExpressionSyntax expression, SyntaxToken closeParen)
        {
            OpenParen = openParen;
            Expression = expression;
            CloseParen = closeParen;
        }

        public SyntaxToken OpenParen { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParen { get; }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create<SyntaxNodeOrToken>(
            OpenParen, Expression, CloseParen);
    }
}
