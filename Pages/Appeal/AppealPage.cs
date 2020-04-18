using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.Appeal;
using WebApp.Facade.Appeals;

namespace WebApp.Pages.Appeal
{
    public abstract class AppealPage : PageModel
    {
        protected internal readonly IAppealRepository _context;

        protected internal AppealPage(IAppealRepository context)
        {
            _context = context;
            PageTitle = "Appeals";
        }

        [BindProperty]
        public AppealView Item { get; set; }
        public IList<AppealView> Items { get; set; }

        public string ItemId => Item.Id;
        public string PageTitle { get; set; }
        public string PageSubTitle { get; set; }
        public string CurrentSort { get; set; } = "Current sort";
        public string CurrentFilter { get; set; } = "Current filter";
        public int PageIndex { get; set; } = 3;
        public int TotalPages { get; set; } = 10;
    }
}
