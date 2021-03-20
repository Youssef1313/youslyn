namespace Youslyn.CodeAnalysis.Diagnostic
{
    // TODO: Have a location.
    public sealed record DiagnosticDescriptor(string Text, DiagnosticSeverity Severity);
}
