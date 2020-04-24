using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Request;
using WebApp.Facade.Request;
using WebApp.Pages.Request;

namespace WebApp.userSupportWebApp.Areas.SupportApp.Pages.Requests
{
    public class IndexModel : RequestPage
    {
        private const string DEFAULT_SORT = "Deadline";

        public IndexModel(IRequestRepository context) : base(context) {}

        public async Task<IActionResult> OnGetMarkDone(string id)
        {
            if (id == null) return NotFound();

            var obj = await _context.Get(id);

            obj.Data.Solved = true;
            await _context.UpdateObject(obj);

            string url = "/SupportApp/Requests";

            return Redirect(url);
        }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (sortOrder == null)
                sortOrder = DEFAULT_SORT;

            sortOrder = string.IsNullOrEmpty(sortOrder) ? "BillNumber" : sortOrder;
            CurrentSort = sortOrder;

            EntryDateSort = sortOrder == "EntryDate" ? "EntryDate_desc" : "EntryDate";
            RequestDeadlineSort = sortOrder == "Deadline" ? "Deadline_desc" : "Deadline";
           

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            _context.SortOrder = sortOrder;
            SearchString = CurrentFilter;
            _context.SearchString = searchString;
            _context.PageIndex = pageIndex ?? 1;
            PageIndex = _context.PageIndex;


            var l = await _context.Get();
            Items = new List<RequestView>();

            foreach (var element in l)
            {
                if (element.Data.Solved == false)
                {
                    Items.Add(RequestViewFactory.Create(element));
                }
                
            }

            HasNextPage = _context.HasNextPage;
            HasPreviousPage = _context.HasPreviousPage;
        }

        public string EntryDateSort { get; private set; }
        public string RequestDeadlineSort { get; set; }
        

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public string SearchString { get; set; }
    }
}
