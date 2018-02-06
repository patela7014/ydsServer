using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Resources
{
    public class SabhaTypeResource
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
