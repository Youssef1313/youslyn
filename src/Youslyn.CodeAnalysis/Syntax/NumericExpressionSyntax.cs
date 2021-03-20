using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal class NumericExpressionSyntax : ExpressionSyntax
    {
        public NumericExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumericExpression;

        public override ImmutableArray<SyntaxNodeOrToken> Children => ImmutableArray.Create(new SyntaxNodeOrToken(NumberToken));

        public SyntaxToken NumberToken { get; }
    }
}
