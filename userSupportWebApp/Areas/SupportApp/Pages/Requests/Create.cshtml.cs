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


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var obj = RequestViewFactory.Create(Item);
            obj.Data.Id = Guid.NewGuid().ToString();
            obj.Data.EntryDate = DateTime.Now;
            // obj.Data.Id = Guid.NewGuid().ToString(); // Lets see, if database will generate it by itself
            await _context.AddObject(obj);

            return RedirectToPage("./Index");
        }
    }
}
