using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAL.Resources
{
    public class SabhaListResource
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SabhaType { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UsersListResource> Users { get; set; }

        public SabhaListResource()
        {
            Users = new Collection<UsersListResource>();
        }
    }
}
