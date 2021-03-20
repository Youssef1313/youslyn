namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, string text, object? value)
        {
            Kind = kind;
            Text = text;
            Value = value;
        }

        public SyntaxKind Kind { get; }
        public string Text { get; }
        public object? Value { get; }
    }
}
