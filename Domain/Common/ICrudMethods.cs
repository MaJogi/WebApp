using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Common
{
    public interface ICrudMethods<TObject>
    {
        Task<TObject> Get(string id);
        Task<IEnumerable<TObject>> Get();
        Task<TObject> AddObject(TObject o);
        Task UpdateObject(TObject o);
        Task DeleteObject(TObject o);
    }
}
