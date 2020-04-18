using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Request;
using WebApp.Facade.Request;
using WebApp.Pages.Request;

namespace WebApp.userSupportWebApp.Areas.SupportApp.Pages.Requests
{
    public class DetailsModel : RequestPage
    {
        public DetailsModel(IRequestRepository context) : base(context) {}

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            Item = RequestViewFactory.Create(await _context.Get(id));

            if (Item == null) return NotFound();

            return Page();
        }
    }
}
