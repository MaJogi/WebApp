using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WebApp.Data.Appeal;
using WebApp.Domain.Appeal;

namespace WebApp.Facade.Appeals
{
    public class AppealViewFactory
    {
        public static Appeal Create(AppealView v)
        {
            var data = new AppealData
            {
                Id = v.Id,
                Description = v.Description,
                EntryDate = v.EntryDate,
                Deadline = v.DeadLine
            };
            return new Appeal(data);
        }

        public static AppealView Create(IAppeal o)
        {
            var obj = o as Appeal;
            Debug.Assert(obj != null, nameof(obj) + " != null");
            var view = new AppealView
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
