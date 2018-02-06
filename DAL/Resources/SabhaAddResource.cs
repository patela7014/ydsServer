using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Resources
{
    public class SabhaAddResource
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SabhaTypeId { get; set; }

        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}
