using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity,bool>>?Criteria {  get; }
        List<Expression<Func<TEntity,Object>>> IncludeExpressions { get; }
        Expression<Func<TEntity,Object>> OrderBy { get; }
        Expression<Func<TEntity,Object>> OrderByDescending { get; }
        public int Take {  get; }
        public int Skip { get; }
        public bool IsPaginated { set; get; } 

    }
}
