using System;
using WebApp.Data.Common;

namespace WebApp.Data.Request
{
    public class RequestData : UniqueEntityData
    {
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? Deadline { get; set; }
        public bool Solved { get; set; }
    }
}
