using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Core.Models
{
    public class EventAttendance
    {
        public string EventId { get; set; }
        public string UserId { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
