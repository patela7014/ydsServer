using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Core.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EventDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; } = "TX";
        public string Country { get; set; } = "USA";
        public bool IsActive { get; set; } = true;
        public string Notes { get; set; }
        public string AddedBy { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("AddedBy")]
        public User User { get; set; }
        public ICollection<EventAttendance> Users { get; set; }

        public Event()
        {
            Users = new Collection<EventAttendance>();
        }
    }
}
