namespace Youslyn.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {
        internal static int GetBinaryPrecedence(SyntaxToken token)
        {
            return token.Kind switch
            {
                SyntaxKind.PlusToken or SyntaxKind.MinusToken => 1,
                SyntaxKind.AsteriskToken or SyntaxKind.SlashToken => 2,
                _ => 0
            };
        }

        internal static int GetUnaryPrecedence(SyntaxToken token)
        {
            return token.Kind switch
            {
                SyntaxKind.PlusToken or SyntaxKind.MinusToken => 3,
                _ => 0
            };
        }
    }
}
