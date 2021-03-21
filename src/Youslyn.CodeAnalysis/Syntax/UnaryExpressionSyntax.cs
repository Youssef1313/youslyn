using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Operand { get; }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create(
            new SyntaxNodeOrToken(OperatorToken), new SyntaxNodeOrToken(Operand));
    }
}
