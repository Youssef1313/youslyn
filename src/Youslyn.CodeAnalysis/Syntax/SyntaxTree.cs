using System.Collections.Immutable;
using Youslyn.CodeAnalysis.Diagnostic;

namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class SyntaxTree
    {
        // Only for now we are capable of parsing expressions.
        public SyntaxTree(ExpressionSyntax root, ImmutableArray<DiagnosticDescriptor> diagnostics)
        {
            Root = root;
            Diagnostics = diagnostics;
        }

        public ExpressionSyntax Root { get; }
        public ImmutableArray<DiagnosticDescriptor> Diagnostics { get; }

        public static SyntaxTree Parse(string text) => new Parser(text).Parse();
    }
}
