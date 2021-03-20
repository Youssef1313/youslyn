using System.Collections.Immutable;
using Youslyn.CodeAnalysis.Diagnostic;

namespace Youslyn.CodeAnalysis.Syntax
{
    public sealed class SyntaxTree
    {
        public SyntaxTree(SyntaxNode root, ImmutableArray<DiagnosticDescriptor> diagnostics)
        {
            Root = root;
            Diagnostics = diagnostics;
        }

        public SyntaxNode Root { get; }
        public ImmutableArray<DiagnosticDescriptor> Diagnostics { get; }
    }
}
