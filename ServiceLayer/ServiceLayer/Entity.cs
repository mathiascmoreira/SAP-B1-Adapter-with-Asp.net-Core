using System;
using System.Linq.Expressions;

namespace ServiceLayer.ServiceLayer
{
    public class Entity<T>
    {
        private readonly EndPointAssembler _endPointAssembler;

        public Entity() {
           
            _endPointAssembler = new EndPointAssembler();
        }

        public Entity<T> Where(Expression<Func<T, bool>> predicate)
        {
            _endPointAssembler.AddWhereClause(predicate);

            return this;
        }
        
        public Entity<T> OrderBy<TSourse, TKey>(Expression<Func<TSourse, TKey>> keySelector)
        {
            _endPointAssembler.AddOrderByProperty(keySelector);

            return this;
        }

        public Entity<T> OrderByDescending<TSourse, TKey>(Expression<Func<TSourse, TKey>> keySelector)
        {
            _endPointAssembler.AddOrderByPropertyDescending(keySelector);

            return this;
        }

        public Entity<T> Top(int top)
        {
            _endPointAssembler.SetTop(top);

            return this;
        }

        public Entity<T> Skip(int skip)
        {
            _endPointAssembler.SetSkip(skip);

            return this;
        }

        public Entity<T> InlineCount()
        {
            _endPointAssembler.SetInlineCount();

            return this;
        }

        public ServiceLayerResult<T> Count()
        {
            return null;
        }

        public ServiceLayerResult<T> ToList()
        {
            return null;
        }

        public ServiceLayerResult<T> Insert<T>(params T[] registers)
        {
            return null;
        }

        public string GetEndPoint()
        {
            return null;
          //  return _endPointAssembler.AssembleEndPoint();
        }
       
    }
}
