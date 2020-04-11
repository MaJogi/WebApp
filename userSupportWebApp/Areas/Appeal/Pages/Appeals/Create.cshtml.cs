using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data.Appeal;
using WebApp.Infra;

namespace userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Infra.SupportAppDbContext _context;

        public CreateModel(WebApp.Infra.SupportAppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppealData AppealData { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Appeals.Add(AppealData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
