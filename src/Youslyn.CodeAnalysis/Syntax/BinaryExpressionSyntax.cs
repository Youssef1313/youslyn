using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal class BinaryExpressionSyntax : ExpressionSyntax
    {
        private readonly ExpressionSyntax _left;
        private readonly SyntaxToken _operatorToken;
        private readonly ExpressionSyntax _right;

        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create(
            new SyntaxNodeOrToken(_left), new SyntaxNodeOrToken(_operatorToken), new SyntaxNodeOrToken(_right));
    }
}
