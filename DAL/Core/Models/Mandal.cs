using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Core.Models
{
    public class Mandal
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; } = "TX";
        public string Country { get; set; } = "USA";
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        public ICollection<User> User { get; set; }

    }
}
