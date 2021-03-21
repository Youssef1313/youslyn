using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Youslyn.CodeAnalysis.Diagnostics
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly ImmutableArray<Diagnostic>.Builder _diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

        public ImmutableArray<Diagnostic> Diagnostics => _diagnostics.ToImmutable();

        public void AddDiagnostic(string text, DiagnosticSeverity severity)
            => _diagnostics.Add(new Diagnostic(text, severity));

        public IEnumerator<Diagnostic> GetEnumerator()
            => _diagnostics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
