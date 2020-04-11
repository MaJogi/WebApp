using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain
{
    public interface IPaging
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get;  }
    }
}
