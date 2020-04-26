using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Request;
using WebApp.Facade.Request;
using WebApp.Pages.Request;

namespace WebApp.userSupportWebApp.Areas.SupportApp.Pages.Requests
{
    public class DeleteModel : RequestPage
    {

        public DeleteModel(IRequestRepository context) : base(context) { }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();
            await getObject(id);
            if (Item == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null) return NotFound();

            await deleteObject(id);

            return RedirectToPage("./Index");
        }
    }
}
