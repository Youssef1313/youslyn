using System;
using Youslyn.CodeAnalysis.Syntax;

namespace Youslyn.CodeAnalysis.ExpressionEvaluator
{
    internal static class Evaluator
    {
        public static int Evaluate(ExpressionSyntax expression)
        {
            switch (expression.Kind)
            {
                case SyntaxKind.NumericExpression:
                    var numericSyntax = (NumericExpressionSyntax)expression;
                    if (numericSyntax.NumberToken.Value is not { } value)
                    {
                        throw new ArgumentException("Unexpected null value of NumericExpressionSyntax.");
                    }

                    return (int)value;
                case SyntaxKind.BinaryExpression:
                    return EvaluateBinaryExpression((BinaryExpressionSyntax)expression);
                case SyntaxKind.UnaryExpression:
                    return EvaluateUnaryExpression((UnaryExpressionSyntax)expression);
                case SyntaxKind.ParenthesizedExpression:
                    return Evaluate(((ParenthesizedExpressionSyntax)expression).Expression);
                default:
                    throw new ArgumentException($"Unexpected expression kind '{expression.Kind}'.");
            }
        }

        private static int EvaluateUnaryExpression(UnaryExpressionSyntax unary)
        {
            return unary.OperatorToken.Kind switch
            {
                SyntaxKind.PlusToken => Evaluate(unary.Operand),
                SyntaxKind.MinusToken => -Evaluate(unary.Operand),
                _ => throw new InvalidOperationException($"Unexpected operator token '{unary.OperatorToken.Kind}'")
            };
        }

        private static int EvaluateBinaryExpression(BinaryExpressionSyntax binary)
        {
            return binary.OperatorToken.Kind switch
            {
                SyntaxKind.PlusToken => Evaluate(binary.Left) + Evaluate(binary.Right),
                SyntaxKind.MinusToken => Evaluate(binary.Left) - Evaluate(binary.Right),
                SyntaxKind.AsteriskToken => Evaluate(binary.Left) * Evaluate(binary.Right),
                SyntaxKind.SlashToken => Evaluate(binary.Left) / Evaluate(binary.Right),
                _ => throw new InvalidOperationException($"Unexpected operator token '{binary.OperatorToken.Kind}'")
            };
        }
    }
}
