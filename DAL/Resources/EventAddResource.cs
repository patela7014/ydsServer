using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Resources
{
    public class EventAddResource
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [System.ComponentModel.DefaultValue(true)]
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }
}
