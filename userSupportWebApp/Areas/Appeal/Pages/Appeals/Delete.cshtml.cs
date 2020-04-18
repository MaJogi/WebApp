using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Appeal;
using WebApp.Facade.Appeals;
using WebApp.Pages.Appeal;

namespace WebApp.userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class DeleteModel : AppealPage
    {

        public DeleteModel(IAppealRepository context) : base(context) { }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            Item = AppealViewFactory.Create(await _context.Get(id));

            if (Item == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null) return NotFound();

            var o = await _context.Get(id);
            await _context.DeleteObject(o);

            return RedirectToPage("./Index");
        }
    }
}
