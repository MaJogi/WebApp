using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Appeal;

namespace WebApp.Domain.Appeal
{
    public class AppealFactory
    {
        public static Appeal Create(
            string description
        )
        {
            var appealData = new AppealData()
            {
                Description = description
            };
            return new Appeal(appealData);
        }
    }
}
