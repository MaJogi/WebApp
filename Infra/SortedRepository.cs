using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Common;
using WebApp.Domain;
using WebApp.Domain.Common;

namespace WebApp.Infra
{
    public abstract class SortedRepository<TObject, TData> : BaseRepository<TObject, TData>, ISorting
        where TData : UniqueEntityData, new()
        where TObject : Entity<TData>, new()
    {
        public string SortOrder { get; set; }
        public string DescendingString => "_desc";
        protected SortedRepository(DbContext context, DbSet<TData> set) : base(context, set) { }

        protected internal override IQueryable<TData> createSqlQuery()
        {
            var query = base.createSqlQuery();
            query = addSorting(query);

            return query;
        }

        protected internal IQueryable<TData> addSorting(IQueryable<TData> query)
        {
            var expression = createExpression();

            var r = expression is null ? query : addOrderBy(query, expression);
            return r;
        }

        internal Expression<Func<TData, object>> createExpression()
        {
            var property = findProperty();

            return property is null ? null : lambdaExpression(property);
        }

        internal Expression<Func<TData, object>> lambdaExpression(PropertyInfo p)
        {
            var param = Expression.Parameter(typeof(TData), "x"); 
            var property = Expression.Property(param, p); 
            var body = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TData, object>>(body, param);
        }

        internal PropertyInfo findProperty()
        {
            var name = getName();
            return typeof(TData).GetProperty(name);
        }

        internal string getName()
        {
            if (string.IsNullOrEmpty(SortOrder)) return string.Empty;
            var index = SortOrder.IndexOf(DescendingString, StringComparison.Ordinal);
            return index > 0 ? SortOrder.Remove(index) : SortOrder;
        }

        internal IQueryable<TData> addOrderBy(IQueryable<TData> query, Expression<Func<TData, object>> e)
        {
            if (query is null) return null;
            if (e is null) return query; 
            try
            {
                return isDecending() ? query.OrderByDescending(e) : query.OrderBy(e);
            }
            catch { return query; }

        }

        internal bool isDecending() => !string.IsNullOrEmpty(SortOrder) && SortOrder.EndsWith(DescendingString);


    }
}
