using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal class UnaryExpressionSyntax : ExpressionSyntax
    {
        private SyntaxToken _operatorToken;
        private ExpressionSyntax _operand;

        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            _operatorToken = operatorToken;
            _operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create(
            new SyntaxNodeOrToken(_operatorToken), new SyntaxNodeOrToken(_operand));
    }
}
