using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Aids;
using WebApp.Data.Request;
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
        public string SearchString { get; set; }

        public bool HasPreviousPage => _context.HasPreviousPage;
        public bool HasNextPage => _context.HasNextPage;
        public int PageIndex 
        { 
            get => _context.PageIndex;
            set => _context.PageIndex = value;
        }
        public int TotalPages => _context.TotalPages;
        private const string DEFAULT_SORT = "Deadline";


        protected internal async Task<bool> addObject()
        {
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            try
            {
                if (!ModelState.IsValid) return false;
                var obj = RequestViewFactory.Create(Item);
                obj.Data.Id = Guid.NewGuid().ToString();
                obj.Data.EntryDate = DateTime.Now;
                await _context.AddObject(obj);
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        protected internal async Task<bool> updateObject()
        {
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            try
            {
                if (!ModelState.IsValid) return false;

                await _context.UpdateObject(RequestViewFactory.Create(Item));
            }
            catch
            {
                return false;
            }

            return true;
        }


        protected internal async Task getObject(string id)
        {
            Item = RequestViewFactory.Create(await _context.Get(id));
        }

        protected internal async Task deleteObject(string id)
        {
            var o = await _context.Get(id);
            await _context.DeleteObject(o);
        }

        public string GetSortString(Expression<Func<RequestData, object>> e, string page)
        {
            //$"{page}?sortOrder={Model.CurrentSort}&currentFilter={Model.CurrentFilter}"
            var name = GetMember.Name(e);
            string sortOrder;
            if (string.IsNullOrEmpty(CurrentSort)) sortOrder = name;
            else if (!CurrentSort.StartsWith(name)) sortOrder = name;
            else if (CurrentSort.EndsWith("_desc")) sortOrder = name;
            else sortOrder = name + "_desc";

            return $"{page}?sortOrder={sortOrder}&currentFilter={CurrentFilter}";
        }

        protected internal async Task getList(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (sortOrder == null)
                sortOrder = DEFAULT_SORT;

            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Deadline" : sortOrder;
            CurrentSort = sortOrder;

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

            PageIndex = pageIndex ?? 1;


            var l = await _context.Get();
            Items = new List<RequestView>();

            foreach (var element in l)
            {
                if (element.Data.Solved == false) Items.Add(RequestViewFactory.Create(element));
            }
        }
    }
}
