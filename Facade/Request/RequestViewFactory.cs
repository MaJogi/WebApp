using System.Diagnostics;
using WebApp.Data.Request;
using WebApp.Domain.Request;

namespace WebApp.Facade.Request
{
    public class RequestViewFactory
    {
        public static Domain.Request.Request Create(RequestView v)
        {
            var data = new RequestData
            {
                Id = v.Id,
                Description = v.Description,
                EntryDate = v.EntryDate,
                Deadline = v.DeadLine
            };
            return new Domain.Request.Request(data);
        }

        public static RequestView Create(IRequest o)
        {
            var obj = o as Domain.Request.Request;
            Debug.Assert(obj != null, nameof(obj) + " != null");
            var view = new RequestView
            {
                Id = obj.Data.Id,
                Description = obj.Data.Description,
                EntryDate = obj.Data.EntryDate,
                DeadLine = obj.Data.Deadline
            };
            return view;
        }
    }
}
