using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Core.Models
{
    public class SabhaUsers
    {
        public string SabhaId { get; set; }
        public string UserId { get; set; }
        public Sabha Sabha { get; set; }
        public User User { get; set; }
    }
}
