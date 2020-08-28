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
        private readonly List<Expression<Func<T, bool>>> _whereClauses;
        private readonly EndPointAssembler _endPointAssembler;

        public Entity() {
            _whereClauses = new List<Expression<Func<T, bool>>>();
            _endPointAssembler = new EndPointAssembler();
        }

        public Entity<T> Where(Expression<Func<T, bool>> expression)
        {
            _whereClauses.Add(expression);

           // var result = GetCommand(expression.Body.ToExpressionNode());

            return this;
        }

        public Entity<T> Select()
        {
            return this;
        }

        public Entity<T> Top()
        {
            return this;
        }

        public Entity<T> OrderBy()
        {
            return this;
        }

        public Entity<T> Skip()
        {
            return this;
        }

        public Entity<T> InlineCount()
        {
            return this;
        }

        public int Count()
        {
            return 0;
        }

        public string GetEndPoint()
        {
            return _endPointAssembler.AssembleEndPoint(_whereClauses);
        }


       
    }
}
