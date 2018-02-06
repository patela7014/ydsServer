using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Core.Models
{
    public class Sabha
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SabhaTypeId { get; set; }

        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; } = "TX";
        public string Country { get; set; } = "USA";
        public bool IsActive { get; set; } = true;

        [ForeignKey("SabhaTypeId")]
        public SabhaType SabhaType { get; set; }
        public ICollection<SabhaUsers> Users { get; set; }

        public Sabha()
        {
            Users = new Collection<SabhaUsers>();
        }
    }
}
