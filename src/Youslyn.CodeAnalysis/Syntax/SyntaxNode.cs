using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }
        public abstract ImmutableArray<SyntaxNodeOrToken> Children { get; }
    }
}
