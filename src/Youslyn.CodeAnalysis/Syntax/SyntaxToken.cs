namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class SyntaxToken
    {
        // text can be null for fabricated tokens. See Parser.Match(SyntaxKind) method.
        public SyntaxToken(SyntaxKind kind, string? text, object? value)
        {
            Kind = kind;
            Text = text ?? throw new System.ArgumentNullException(nameof(text));
            Value = value;
        }

        public SyntaxKind Kind { get; }
        public string? Text { get; }
        public object? Value { get; }
    }
}
