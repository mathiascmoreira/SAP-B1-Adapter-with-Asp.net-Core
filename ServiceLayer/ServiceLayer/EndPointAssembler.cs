using Serialize.Linq.Extensions;
using Serialize.Linq.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceLayer.ServiceLayer
{
    public class EndPointAssembler
    {
        public string AssembleEndPoint<T>(List<Expression<Func<T, bool>>> whereClauses)
        {
            return GetCommand(whereClauses.First().ToExpressionNode());
        }

        private string GetCommand(ExpressionNode expressionNode)
        {
            switch (expressionNode.NodeType)
            {
                case ExpressionType.AndAlso:
                    return EvaluateBinaryExpression(expressionNode, "and", true);                  
                case ExpressionType.OrElse:
                    return EvaluateBinaryExpression(expressionNode, "or");
                case ExpressionType.GreaterThan:
                    return EvaluateBinaryExpression(expressionNode, "gt");
                case ExpressionType.GreaterThanOrEqual:
                    return EvaluateBinaryExpression(expressionNode, "ge");
                case ExpressionType.LessThan:
                    return EvaluateBinaryExpression(expressionNode, "lt");
                case ExpressionType.LessThanOrEqual:
                    return EvaluateBinaryExpression(expressionNode, "le");
                case ExpressionType.Equal:
                    return EvaluateBinaryExpression(expressionNode, "eq");
                case ExpressionType.NotEqual:
                    return EvaluateBinaryExpression(expressionNode, "ne");
                case ExpressionType.Constant:
                    return EvaluateConstantExpression(expressionNode);
                case ExpressionType.MemberAccess:
                    return EvaluateMemberExpression(expressionNode);                    
                case ExpressionType.Convert:
                    return EvaluateConvertExpression(expressionNode);
                case ExpressionType.Not:
                    return EvaluateNotExpression(expressionNode);
                case ExpressionType.Call:
                    return EvaluateMethodCallExpression(expressionNode);
                default:
                    throw new Exception("Invalid Expression");
            }
        }

        private string EvaluateBinaryExpression(ExpressionNode expressionNode, string op, bool parentheses = false)
        {
            var binaryExpression = (BinaryExpressionNode)expressionNode;

            return parentheses ? 
                $"({GetCommand(binaryExpression.Left)}) {op} ({GetCommand(binaryExpression.Right)})" : 
                $"{GetCommand(binaryExpression.Left)} {op} {GetCommand(binaryExpression.Right)}";
        }

        private string EvaluateConstantExpression(ExpressionNode expressionNode)
        {
            var constantExpression = (ConstantExpressionNode)expressionNode;

            switch (constantExpression.Type.Name)
            {
                case "System.String":
                case "System.Char":
                    return $"'{constantExpression.Value}'";
                case "System.Boolean":
                case "System.Byte":
                case "System.SByte":
                case "System.Decimal":
                case "System.Double":
                case "System.Single":
                case "System.Float":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int64":
                case "System.UInt64":
                case "System.Int16":
                case "System.UInt16":
                    return constantExpression.Value.ToString();
                default:
                    throw new Exception("Invalid Expression");
            }
        }

        private string EvaluateMemberExpression(ExpressionNode expressionNode)
        {
            return ((MemberExpressionNode)expressionNode).Member.Signature.Split(' ')[1];
        }

        private string EvaluateConvertExpression(ExpressionNode expressionNode)
        {
            return GetCommand(((UnaryExpressionNode)expressionNode).Operand);
        }

        private string EvaluateNotExpression(ExpressionNode expressionNode)
        {
            return $"not ({GetCommand(((UnaryExpressionNode)expressionNode).Operand)})";
        }

        private string EvaluateMethodCallExpression(ExpressionNode expressionNode)
        {
            var methodCallExpression = (MethodCallExpressionNode)expressionNode;

            if (methodCallExpression.Object.Type.Name != "System.String")
                throw new Exception("Method Call are only accepted from strings");

            return EvaluateStringMethodCall(methodCallExpression);
        }

        private string EvaluateStringMethodCall(MethodCallExpressionNode methodCallExpression)
        {
            var memberExpression = (MemberExpressionNode)methodCallExpression.Object;
            var constantExpression = (ConstantExpressionNode)methodCallExpression.Arguments.First();

            switch (methodCallExpression.Method.Signature)
            {
                case "Boolean Equals(System.String)":
                    return $"{EvaluateMemberExpression(memberExpression)} eq '{constantExpression.Value}'";
                case "Boolean StartsWith(System.String)":
                    return $"startswith({EvaluateMemberExpression(memberExpression)}, '{constantExpression.Value}')";
                case "Boolean EndsWith(System.String)":
                    return $"endswith({EvaluateMemberExpression(memberExpression)}, '{constantExpression.Value}')";
                case "Boolean Contains(System.String)":
                    return $"contains({EvaluateMemberExpression(memberExpression)}, '{constantExpression.Value}')";
                default:
                    throw new Exception("string only supports Equals, StartsWith, EndsWith and Contains methods");
            }
        }
    }
}
