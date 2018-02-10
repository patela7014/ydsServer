using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAL.Resources
{
    public class AddSabhaUserResource
    {
        public string SabhaId { get; set; }
        public ICollection<string> IncludedUsers { get; set; }
        public ICollection<string> ExcludedUsers { get; set; }

        public AddSabhaUserResource()
        {
            IncludedUsers = new Collection<string>();
            ExcludedUsers = new Collection<string>();
        }
    }
}
