using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Syntax
{
    public abstract class SyntaxNode
    {
        private SyntaxNodeOrToken? _nodeOrToken;

        public abstract SyntaxKind Kind { get; }
        public abstract ImmutableArray<SyntaxNodeOrToken> Children { get; }

        public SyntaxNodeOrToken NodeOrToken
        {
            get
            {
                return _nodeOrToken is null
                    ? _nodeOrToken = new SyntaxNodeOrToken(this)
                    : _nodeOrToken;
            }
        }
    }
}
