using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Appeal;
using WebApp.Infra;

namespace userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class EditModel : PageModel
    {
        private readonly WebApp.Infra.SupportAppDbContext _context;

        public EditModel(WebApp.Infra.SupportAppDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AppealData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppealDataExists(AppealData.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AppealDataExists(string id)
        {
            return _context.Appeals.Any(e => e.Id == id);
        }
    }
}
