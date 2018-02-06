using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAL.Resources
{
    public class AddAttendanceResource
    {
        public string EventId { get; set; }
        public ICollection<string> Users { get; set; }
        public AddAttendanceResource()
        {
            Users = new Collection<string>();
        }
    }
}
