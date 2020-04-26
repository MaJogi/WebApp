using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Aids;
using WebApp.Data.Request;
using WebApp.Domain.Request;
using WebApp.Facade.Request;
using WebApp.Pages.Request;

namespace WebApp.userSupportWebApp.Areas.SupportApp.Pages.Requests
{
    public class IndexModel : RequestPage
    {
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
            await getList(sortOrder,
            currentFilter, searchString, pageIndex);
        }
    }
}
