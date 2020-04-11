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
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Infra.SupportAppDbContext _context;

        public DeleteModel(WebApp.Infra.SupportAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppealData AppealData { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppealData = await _context.Appeals.FirstOrDefaultAsync(m => m.Id == id);

            if (AppealData == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppealData = await _context.Appeals.FindAsync(id);

            if (AppealData != null)
            {
                _context.Appeals.Remove(AppealData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
