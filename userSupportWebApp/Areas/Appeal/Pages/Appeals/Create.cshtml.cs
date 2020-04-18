using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Appeal;
using WebApp.Facade.Appeals;
using WebApp.Pages.Appeal;

namespace WebApp.userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class CreateModel : AppealPage
    {

        public CreateModel(IAppealRepository context) : base(context){ }
        public IActionResult OnGet() => Page();


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var obj = AppealViewFactory.Create(Item);
            obj.Data.Id = Guid.NewGuid().ToString();
            obj.Data.EntryDate = DateTime.Now;
            // obj.Data.Id = Guid.NewGuid().ToString(); // Lets see, if database will generate it by itself
            await _context.AddObject(obj);

            return RedirectToPage("./Index");
        }
    }
}
