namespace Youslyn.CodeAnalysis.Diagnostics
{
    // TODO: Have a location.
    public sealed record Diagnostic(string Text, DiagnosticSeverity Severity);
}
