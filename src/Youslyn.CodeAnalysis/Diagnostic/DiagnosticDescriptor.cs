namespace Youslyn.CodeAnalysis.Diagnostic
{
    public sealed class DiagnosticDescriptor
    {
        // TODO: Have a location.
        public DiagnosticDescriptor(string text, DiagnosticSeverity severity)
        {
            Text = text;
            Severity = severity;
        }

        public string Text { get; }
        public DiagnosticSeverity Severity { get; }
    }
}