namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class SyntaxToken
    {
        private SyntaxNodeOrToken? _nodeOrToken;

        // text can be null for fabricated tokens. See Parser.Match(SyntaxKind) method.
        public SyntaxToken(SyntaxKind kind, string? text, object? value)
        {
            Kind = kind;
            Text = text;
            Value = value;
        }

        public SyntaxKind Kind { get; }
        public string? Text { get; }
        public object? Value { get; }

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
