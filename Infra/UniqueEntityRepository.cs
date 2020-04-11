using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Common;
using WebApp.Domain.Common;

namespace WebApp.Infra
{
    public abstract class UniqueEntityRepository<TObject, TData> : PaginatedRepository<TObject, TData>
        where TData : UniqueEntityData, new()
        where TObject : Entity<TData>, new()
    {
        protected UniqueEntityRepository(DbContext context, DbSet<TData> set) : base(context, set) { }

        protected override async Task<TData> getData(string id)
        {
            return await dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
