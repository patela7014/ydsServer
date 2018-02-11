using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Resources
{
    public class AddressResource
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}
