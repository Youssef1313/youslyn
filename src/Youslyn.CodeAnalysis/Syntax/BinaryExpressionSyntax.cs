using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create<SyntaxNodeOrToken>(
            Left, OperatorToken, Right);

        public ExpressionSyntax Left { get; }

        public SyntaxToken OperatorToken { get; }

        public ExpressionSyntax Right { get; }
    }
}
