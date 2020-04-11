using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Data.Appeal;
using WebApp.Domain.Appeal;

namespace WebApp.Infra.Appeal
{
    public class AppealRepository: UniqueEntityRepository<Domain.Appeal.Appeal, AppealData>, IAppealRepository
    {
        public AppealRepository(SupportAppDbContext context) : base(context,
            context.Appeals) { }

        protected internal override Domain.Appeal.Appeal toDomainObject(AppealData data) => new Domain.Appeal.Appeal(data);

        protected internal override IQueryable<AppealData> addFiltering(IQueryable<AppealData> query)
        {
            if (string.IsNullOrEmpty(SearchString)) return query;
            return query.Where(s => s.Description.Contains(SearchString)
                                    || s.EntryDate != null && s.EntryDate.ToString().Contains(SearchString)
                                    || s.SolvedDate != null && s.SolvedDate.ToString().Contains(SearchString)
                                    );
        }
    }
}
