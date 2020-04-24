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
    public abstract class PaginatedRepository<TObject, TData> : FilteredRepository<TObject, TData>, IPaging
        where TData : UniqueEntityData, new()
        where TObject : Entity<TData>, new()
    {
        public int PageSize { get; set; } = 100;
        public int TotalPages => GetTotalPages(PageSize);

        public int PageIndex { get; set; }
        public bool HasNextPage => PageIndex < TotalPages;
        public bool HasPreviousPage => PageIndex > 1;

        protected PaginatedRepository(DbContext context, DbSet<TData> set) : base(context, set) { }

        internal int GetTotalPages(in int pageSize)
        {
            var count = getItemsCount();
            var pages = countTotalPages(count, pageSize);
            return pages;
        }

        internal int countTotalPages(int count, in int pageSize) => (int)Math.Ceiling(count / (double) pageSize);

        internal int getItemsCount()
        {
            var query = base.createSqlQuery();
            return query.CountAsync().Result;
        }

        protected internal override IQueryable<TData> createSqlQuery() => addSkipAndTake(base.createSqlQuery());

        private IQueryable<TData> addSkipAndTake(IQueryable<TData> query) => query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize);
    }
}
