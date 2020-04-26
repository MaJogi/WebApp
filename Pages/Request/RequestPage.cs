using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.Request;
using WebApp.Facade.Request;

namespace WebApp.Pages.Request
{
    public abstract class RequestPage : PageModel
    {
        protected internal readonly IRequestRepository _context;

        protected internal RequestPage(IRequestRepository context)
        {
            _context = context;
            PageTitle = "Requests";
        }

        [BindProperty]
        public RequestView Item { get; set; }
        public IList<RequestView> Items { get; set; }

        public string ItemId => Item.Id;
        public string PageTitle { get; set; }
        public string PageSubTitle { get; set; }
        public string CurrentSort { get; set; } = "Current sort";
        public string CurrentFilter { get; set; } = "Current filter";
        public int PageIndex { get; set; } = 3;
        public int TotalPages { get; set; } /*= 10;*/
    }
}
