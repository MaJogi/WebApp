using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Facade.Request
{
    public class RequestView
    {
        public string Id { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of entry")]
        public DateTime? EntryDate { get; set; }
        [DisplayName("Request deadline")]
        public DateTime? DeadLine { get; set; }
        public bool Solved { get; set; }
        public bool ExpiringOrHasExpired { get; set; }
    }
}
