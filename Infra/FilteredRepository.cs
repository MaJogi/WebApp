using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Common;
using WebApp.Domain;
using WebApp.Domain.Common;

namespace WebApp.Infra
{
    public abstract class FilteredRepository<TObject, TData> : SortedRepository<TObject, TData>, ISearching
        where TData: UniqueEntityData, new()
        where TObject : Entity<TData>, new()
    {
        protected FilteredRepository(DbContext context, DbSet<TData> set) : base(context, set) { }
        public string SearchString { get; set; }

        protected internal override IQueryable<TData> createSqlQuery()
        {
            var query = base.createSqlQuery();
            query = addFiltering(query);
            return query;
        }

        // If not overridden, will return unfiltered query.
        protected internal virtual IQueryable<TData> addFiltering(IQueryable<TData> query)
        {
            return query;
        }
    }
}
