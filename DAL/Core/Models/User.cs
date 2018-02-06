using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Core.Models
{
    public class User : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
        public bool IsAdmin { get; set; }

        public string MidName { get; set; }
        public string HomePhone { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string MandalId { get; set; }
        public string UserTypeId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Created { get; set; }
        public string BirthDay { get; set; }
        public string BirthMonth { get; set; }
        public string BirthYear { get; set; }
        public string AddedBy { get; set; }
        public string Comments { get; set; }

        public Mandal Mandal { get; set; }
        public UserType UserType { get; set; }

        [ForeignKey("AddedBy")]
        public User AdminId { get; set; }

        public ICollection<SabhaUsers> Sabhas { get; set; }
        public ICollection<EventAttendance> Attendance { get; set; }

        public User()
        {
            Sabhas = new Collection<SabhaUsers>();
            Attendance = new Collection<EventAttendance>();
        }
    }
}
