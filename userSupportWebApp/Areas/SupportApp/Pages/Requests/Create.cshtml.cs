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
            //if (!ModelState.IsValid) return Page();

            //var obj = RequestViewFactory.Create(Item);
            //obj.Data.Id = Guid.NewGuid().ToString();
            //obj.Data.EntryDate = DateTime.Now;
            //await _context.AddObject(obj);

            return RedirectToPage("./Index");
        }
    }
}
