using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Serialize.Linq.Extensions;
using Serialize.Linq.Nodes;
using Serialize.Linq.Serializers;

namespace ServiceLayer.ServiceLayer
{
    public class Entity<T>
    {
        public string Where(Expression<Func<T, bool>> expression)
        {
            var expressionNode = expression.Body.ToExpressionNode();

            var result = GetCommand(expressionNode);

            return result;
        }

        private string GetCommand(ExpressionNode expressionNode)
        {
            switch (expressionNode.NodeType)
            {
                case ExpressionType.AndAlso:
                    return $"({GetCommand(((BinaryExpressionNode)expressionNode).Left)}) and ({GetCommand(((BinaryExpressionNode)expressionNode).Right)})";
                case ExpressionType.OrElse:                                  
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} or {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.GreaterThan:                             
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} gt {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.GreaterThanOrEqual:                      
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} ge {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.LessThan:                                
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} lt {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.LessThanOrEqual:                         
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} le {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.Equal:                                   
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} eq {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.NotEqual:
                    return $"{GetCommand(((BinaryExpressionNode)expressionNode).Left)} ne {GetCommand(((BinaryExpressionNode)expressionNode).Right)}";
                case ExpressionType.Constant:
                    return GetConstant((ConstantExpressionNode)expressionNode);
                case ExpressionType.MemberAccess:
                    return ((MemberExpressionNode)expressionNode).Member.Signature.Split(' ')[1];
                case ExpressionType.Convert:
                    return GetCommand(((UnaryExpressionNode)expressionNode).Operand);
                case ExpressionType.Not:
                    return $"not ({GetCommand(((UnaryExpressionNode)expressionNode).Operand)})";
                case ExpressionType.Call:
                    return GetCommandFromCall((MethodCallExpressionNode)expressionNode);
                default:
                    return null;
            }
        }

        private string GetCommandFromCall(MethodCallExpressionNode expressionNode)
        {
            if (expressionNode.Object.Type.Name == "System.String")
            {
                switch (expressionNode.Method.Signature)
                {
                    case "Boolean Equals(System.String)":
                        return $"{((MemberExpressionNode)expressionNode.Object).Member.Signature.Split(' ')[1]} eq '{((ConstantExpressionNode)expressionNode.Arguments.FirstOrDefault()).Value}'";
                    case "Boolean StartsWith(System.String)":
                        return $"startswith({((MemberExpressionNode)expressionNode.Object).Member.Signature.Split(' ')[1]}, '{((ConstantExpressionNode)expressionNode.Arguments.FirstOrDefault()).Value}')";
                    case "Boolean EndsWith(System.String)":
                        return $"endswith({((MemberExpressionNode)expressionNode.Object).Member.Signature.Split(' ')[1]}, '{((ConstantExpressionNode)expressionNode.Arguments.FirstOrDefault()).Value}')";
                    case "Boolean Contains(System.String)":
                        return $"contains({((MemberExpressionNode)expressionNode.Object).Member.Signature.Split(' ')[1]}, '{((ConstantExpressionNode)expressionNode.Arguments.FirstOrDefault()).Value}')";
                    default:
                        throw new Exception("Unsupported");
                }
            }
            return string.Empty;
        }

        private string GetConstant(ConstantExpressionNode expressionNode)
        {
            switch (expressionNode.Type.Name)
            {
                case "System.String":
                case "System.Char":
                    return $"'{expressionNode.Value}'";
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
                    return expressionNode.Value.ToString();
                default:
                    throw new Exception("Expressao inválida");
            }
        }
    }
}
