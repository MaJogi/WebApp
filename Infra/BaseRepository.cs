using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Common;
using WebApp.Domain.Common;

namespace WebApp.Infra
{
    public abstract class BaseRepository<TObject, TData> : ICrudMethods<TObject>
        where TData : UniqueEntityData, new()
        where TObject : Entity<TData>, new()
    {
        protected internal readonly DbContext context;

        protected internal DbSet<TData> dbSet;

        protected BaseRepository(DbContext context, DbSet<TData> set)
        {
            this.context = context as RequestDbContext;
            dbSet = set;
        }

        public async Task<TObject> AddObject(TObject o)
        {
            Debug.Assert(o != null, nameof(o) + " != null");
            dbSet.Add(o.Data);
            await context.SaveChangesAsync();
            return o;
        }

        public async Task DeleteObject(TObject o)
        {
            if (o == null) return;
            dbSet.Remove(o.Data);
            await context.SaveChangesAsync();
        }

        public async Task<TObject> Get(string id)
        {
            if (id is null) return new TObject();
            var data = await getData(id);

            var obj = new TObject { Data = data };
            return obj;

        }

        public async Task<IEnumerable<TObject>> Get()
        {
            var query = createSqlQuery();
            var set = await runSqlQueryAsync(query);
            return toDomainObjectsList(set);
        }

        public async Task UpdateObject(TObject o)
        {
            try
            {
                Debug.Assert(o != null, nameof(o) + " != null");
                dbSet.Update(o.Data);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        internal List<TObject> toDomainObjectsList(List<TData> set) => set.Select(el => toDomainObject(el)).ToList();
        protected internal abstract TObject toDomainObject(TData periodData);

        internal async Task<List<TData>> runSqlQueryAsync(IQueryable<TData> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        protected internal virtual IQueryable<TData> createSqlQuery()
        {
            var query = from s in dbSet select s;

            return query;
        }

        protected abstract Task<TData> getData(string id);
    }
}
