using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Facade.Appeals
{
    public class AppealView
    {
        public string Id { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of entry")]
        public DateTime? EntryDate { get; set; }
        [DisplayName("Request deadline")]
        public DateTime? DeadLine { get; set; }
    }
}
