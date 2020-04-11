using WebApp.Data.Appeal;
using WebApp.Domain.Common;

namespace WebApp.Domain.Appeal
{
    public class Appeal : Entity<AppealData>, IAppeal
    {
        public Appeal(AppealData data) : base(data) { }

        public Appeal() : this(null) { }

    }
}
