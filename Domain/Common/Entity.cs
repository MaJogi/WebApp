using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Common;

namespace WebApp.Domain.Common
{
    public abstract class Entity<TData> where TData: UniqueEntityData, new()
    {
        protected internal Entity(TData d = null) => Data = d ?? new TData();
        public TData Data { get; internal set; }
    }
}
