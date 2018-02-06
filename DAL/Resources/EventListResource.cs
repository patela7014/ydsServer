using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAL.Resources
{
    public class EventListResource
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EventDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        public ICollection<AttendanceResource> Attendance { get; set; }
        public ICollection<UsersListResource> AllUsers { get; set; }

        public EventListResource()
        {
            Attendance = new Collection<AttendanceResource>();
            AllUsers = new Collection<UsersListResource>();
        }
    }
}
