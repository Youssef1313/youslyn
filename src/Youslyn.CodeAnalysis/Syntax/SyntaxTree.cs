using System;
using System.Collections.Immutable;
using System.Linq;
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

        public static void PrettyPrint(SyntaxNode root)
        {
            PrettyPrint(new SyntaxNodeOrToken(root), string.Empty, true);
        }

        private static void PrettyPrint(SyntaxNodeOrToken nodeOrToken, string indent, bool isLast)
        {
            var marker = isLast ? "└── " : "├── ";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(nodeOrToken.Kind);

            if (nodeOrToken.IsToken() && nodeOrToken.AsToken()!.Value is object value)
            {
                Console.Write(" ");
                Console.Write(value);
            }

            Console.WriteLine();

            if (nodeOrToken.IsToken())
            {
                return;
            }

            indent += isLast ? "    " : "│   ";
            var lastChild = nodeOrToken.AsNode()!.Children.Last();

            foreach (var child in nodeOrToken.AsNode()!.Children)
                PrettyPrint(child, indent, lastChild.Equals(child));
        }
    }
}
