﻿using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Common;

namespace WebApp.Data.Appeal
{
    public class AppealData : UniqueEntityData
    {
        public string Description { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public DateTime SolvedDate { get; set; }
    }
}