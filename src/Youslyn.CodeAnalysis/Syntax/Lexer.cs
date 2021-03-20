using System;
using System.Collections.Immutable;
using Youslyn.CodeAnalysis.Diagnostic;

namespace Youslyn.CodeAnalysis.Syntax
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private readonly ImmutableArray<DiagnosticDescriptor>.Builder _diagnosticsBuilder = ImmutableArray.CreateBuilder<DiagnosticDescriptor>();

        public Lexer(string text)
        {
            _text = text;
        }

        private char Current
            => _position < _text.Length ? _text[_position] : '\0';

        public ImmutableArray<DiagnosticDescriptor> Diagnostics => _diagnosticsBuilder.ToImmutableArray();

        public SyntaxToken EatToken()
        {
            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                int start = _position;
                while (char.IsDigit(Current))
                {
                    _position++;
                }

                string numberText = _text[start.._position];
                if (!int.TryParse(numberText, out int value))
                {
                    AddError($"Token '{numberText}' cannot be parsed as an integer.");
                }

                return new SyntaxToken(SyntaxKind.NumericLiteralToken, numberText, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                int start = _position;
                while (char.IsWhiteSpace(Current))
                {
                    _position++;
                }

                return new SyntaxToken(SyntaxKind.WhitespaceToken, _text[start.._position], null);
            }

            if (Current == '+')
            {
                _position++;
                return new SyntaxToken(SyntaxKind.PlusToken, "+", null);
            }

            if (Current == '-')
            {
                _position++;
                return new SyntaxToken(SyntaxKind.MinusToken, "-", null);
            }

            if (Current == '*')
            {
                _position++;
                return new SyntaxToken(SyntaxKind.AsteriskToken, "*", null);
            }

            if (Current == '/')
            {
                _position++;
                return new SyntaxToken(SyntaxKind.SlashToken, "/", null);
            }

            if (Current == '(')
            {
                _position++;
                return new SyntaxToken(SyntaxKind.OpenParenToken, "(", null);
            }

            if (Current == ')')
            {
                _position++;
                return new SyntaxToken(SyntaxKind.CloseParenToken, ")", null);
            }

            string unexpected = Current.ToString();
            AddError($"Unexpected character '{unexpected}'.");
            _position++;
            return new SyntaxToken(SyntaxKind.BadToken, unexpected, null);
        }

        private void AddError(string message)
        {
            _diagnosticsBuilder.Add(new DiagnosticDescriptor(message, DiagnosticSeverity.Error));
        }
    }
}
