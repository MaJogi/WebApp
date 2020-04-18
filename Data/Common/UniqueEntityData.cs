using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Core;

namespace WebApp.Data.Common
{
    public class UniqueEntityData : Archetype
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}
