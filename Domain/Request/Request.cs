using WebApp.Data.Request;
using WebApp.Domain.Common;

namespace WebApp.Domain.Request
{
    public sealed class Request : Entity<RequestData>, IRequest
    {
        public Request(RequestData data) : base(data) { }

        public Request() : this(null) { }

    }
}
