using System.Linq;
using WebApp.Data.Request;
using WebApp.Domain.Request;

namespace WebApp.Infra.Request
{
    public class RequestRepository: UniqueEntityRepository<Domain.Request.Request, RequestData>, IRequestRepository
    {
        public RequestRepository(RequestDbContext context) : base(context,
            context.Requests) { }
        protected internal override Domain.Request.Request toDomainObject(RequestData data) => new Domain.Request.Request(data);

        protected internal override IQueryable<RequestData> addFiltering(IQueryable<RequestData> query)
        {
            query = query.Where(s => s.Solved == false);
            if (string.IsNullOrEmpty(SearchString)) return query;
            return query.Where(s => s.Description.Contains(SearchString)
                                    || s.EntryDate != null && s.EntryDate.ToString().Contains(SearchString)
                                    || s.Deadline != null && s.Deadline.ToString().Contains(SearchString)
                                    );
        }
    }
}
