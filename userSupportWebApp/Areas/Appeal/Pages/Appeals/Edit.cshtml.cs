using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Appeal;
using WebApp.Facade.Appeals;
using WebApp.Pages.Appeal;

namespace WebApp.userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class EditModel : AppealPage
    {

        public EditModel(IAppealRepository context) : base(context) { }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            Item = AppealViewFactory.Create(await _context.Get(id));

            if (Item == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _context.UpdateObject(AppealViewFactory.Create(Item));

            return RedirectToPage("./Index");
        }
    }
}
