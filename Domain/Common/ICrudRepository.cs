using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain.Common
{
    public interface ICrudRepository<TObject> : ICrudMethods<TObject>, IPaging, ISorting, ISearching
    {

    }
}
