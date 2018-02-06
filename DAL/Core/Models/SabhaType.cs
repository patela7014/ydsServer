using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Core.Models
{
    public class SabhaType
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Sabha> Sabha { get; set; }

    }
}
