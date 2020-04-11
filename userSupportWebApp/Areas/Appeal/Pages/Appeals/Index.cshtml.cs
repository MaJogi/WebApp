using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Appeal;
using WebApp.Infra;

namespace userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Infra.SupportAppDbContext _context;

        public IndexModel(WebApp.Infra.SupportAppDbContext context)
        {
            _context = context;
        }

        public IList<AppealData> AppealData { get;set; }

        public async Task OnGetAsync()
        {
            AppealData = await _context.Appeals.ToListAsync();
        }
    }
}
