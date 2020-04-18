﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Domain.Appeal;
using WebApp.Facade.Appeals;
using WebApp.Pages.Appeal;

namespace WebApp.userSupportWebApp.Areas.Appeal.Pages.Appeals
{
    public class IndexModel : AppealPage
    {


        public IndexModel(IAppealRepository context) : base(context) {}

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "BillNumber" : sortOrder;
            CurrentSort = sortOrder;

            BillNumberSort = sortOrder == "BillNumber" ? "BillNumber_desc" : "BillNumber";
            CountryIdSort = sortOrder == "CountryId" ? "CountryId_desc" : "CountryId";
            DeliveryNumberSort = sortOrder == "DeliveryNumber" ? "DeliveryNumber_desc" : "DeliveryNumber";

            EstimatedArrivalSort = sortOrder == "EstimatedArrivalDate" ? "EstimatedArrivalDate_desc" : "EstimatedArrivalDate";
            EstimatedReadyDateSort = sortOrder == "EstimatedReadyDate" ? "EstimatedReadyDate_desc" : "EstimatedReadyDate";
            ShipmentReportCreationDateSort = sortOrder == "ShipmentReportCreationDate" ? "ShipmentReportCreationDate_desc" : "ShipmentReportCreationDate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            _context.SortOrder = sortOrder;
            SearchString = CurrentFilter;
            _context.SearchString = searchString;
            _context.PageIndex = pageIndex ?? 1;
            PageIndex = _context.PageIndex;


            var l = await _context.Get();
            Items = new List<AppealView>();

            foreach (var element in l) { Items.Add(AppealViewFactory.Create(element)); }

            HasNextPage = _context.HasNextPage;
            HasPreviousPage = _context.HasPreviousPage;
        }

        public string DeliveryNumberSort { get; private set; }
        public string BillNumberSort { get; set; }
        public string CountryIdSort { get; private set; }

        public string ShipmentReportCreationDateSort { get; set; }

        public string EstimatedReadyDateSort { get; set; }

        public string EstimatedArrivalSort { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public string SearchString { get; set; }
    }
}
