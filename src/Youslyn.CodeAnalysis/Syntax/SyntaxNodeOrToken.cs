using System;

namespace Youslyn.CodeAnalysis.Syntax
{
    /// <summary>
    /// A class that represents either a <see cref="SyntaxNode"/> or a <see cref="SyntaxToken"/>.
    /// </summary>
    public sealed class SyntaxNodeOrToken : IEquatable<SyntaxNodeOrToken>
    {
        private readonly SyntaxNode? _node;
        private readonly SyntaxToken? _token;
        private readonly bool _isNode;

        /// <summary>
        /// Constructs a <see cref="SyntaxNodeOrToken"/> with a given <see cref="SyntaxNode"/>.
        /// </summary>
        /// <remarks>
        /// If you call <see cref="IsNode"/> on the constructed object, it's always true.
        /// If you call <see cref="IsToken"/> on the constructed object, it's always false.
        /// </remarks>
        public SyntaxNodeOrToken(SyntaxNode node)
        {
            _node = node ?? throw new ArgumentNullException(nameof(node));
            Kind = node.Kind;
            _isNode = true;
        }

        /// <summary>
        /// Constructs a <see cref="SyntaxNodeOrToken"/> with a given <see cref="SyntaxToken"/>
        /// </summary>
        /// <remarks>
        /// If you call <see cref="IsToken"/> on the constructed object, it's always true.
        /// If you call <see cref="IsNode"/> on the constructed object, it's always false.
        /// </remarks>
        public SyntaxNodeOrToken(SyntaxToken token)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            Kind = token.Kind;
        }

        public SyntaxKind Kind { get; }

        /// <summary>
        /// Returns true if this object was constructed using a <see cref="SyntaxNode"/>.
        /// </summary>
        public bool IsNode() => _isNode;

        /// <summary>
        /// Returns true if this object was constructed using a <see cref="SyntaxToken"/>.
        /// </summary>
        public bool IsToken() => !_isNode;

        /// <summary>
        /// Returns the <see cref="SyntaxNode"/> used to construct this object, or null if this was constructed using a <see cref="SyntaxToken"/>.
        /// </summary>
        public SyntaxNode? AsNode() => _node;

        /// <summary>
        /// Returns the <see cref="SyntaxToken"/> used to construct this object, or null if this was constructed using a <see cref="SyntaxNode"/>.
        /// </summary>
        public SyntaxToken? AsToken() => _token;

        public override bool Equals(object? obj)
        {
            return obj is SyntaxNodeOrToken nodeOrToken && Equals(nodeOrToken);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_node, _token, _isNode, Kind);
        }

        public bool Equals(SyntaxNodeOrToken? other)
        {
            if (other is null)
            {
                return false;
            }

            if (IsNode() && other.IsNode())
            {
                return AsNode()!.Equals(other.AsNode());
            }

            if (IsToken() && other.IsToken())
            {
                return AsToken()!.Equals(other.AsToken());
            }

            return false;
        }

        public static implicit operator SyntaxNodeOrToken(SyntaxNode node) => node.NodeOrToken;
        public static implicit operator SyntaxNodeOrToken(SyntaxToken token) => token.NodeOrToken;
    }
}
