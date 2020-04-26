using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Request;
using WebApp.Facade.Request;
using WebApp.Pages.Request;

namespace WebApp.userSupportWebApp.Areas.SupportApp.Pages.Requests
{
    public class CreateModel : RequestPage
    {

        public CreateModel(IRequestRepository context) : base(context){ }
        public IActionResult OnGet() => Page();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!await addObject()) return Page();

            return RedirectToPage("./Index");
        }
    }
}
