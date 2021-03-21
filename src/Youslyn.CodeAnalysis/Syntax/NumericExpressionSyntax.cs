using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class NumericExpressionSyntax : ExpressionSyntax
    {
        public NumericExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumericExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create<SyntaxNodeOrToken>(NumberToken);

        public SyntaxToken NumberToken { get; }
    }
}
