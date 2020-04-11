using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Common;

namespace WebApp.Domain.Appeal
{
    public interface IAppealRepository : ICrudRepository<Appeal>
    {
    }
}
