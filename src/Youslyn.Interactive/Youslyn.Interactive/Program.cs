using System;
using Youslyn.CodeAnalysis.Syntax;

namespace Youslyn.Interactive
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line is null)
                {
                    continue;
                }
                
                SyntaxTree tree = SyntaxTree.Parse(line);
                SyntaxTree.PrettyPrint(tree.Root);

                Console.WriteLine();

                foreach (var diagnostic in tree.Diagnostics)
                {
                    Console.WriteLine(diagnostic.Text);
                }
            }
        }
    }
}
