using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            //if (id == null) return NotFound();

            Item = RequestViewFactory.Create(await _context.Get(id));

            //if (Item == null) return NotFound();
        }

        protected internal async Task deleteObject(string id)
        {
            var o = await _context.Get(id);
            await _context.DeleteObject(o);
        }

        
    }
}
