using System.ComponentModel.DataAnnotations;
using System.DAL.Models.Identity;

namespace System.DAL.Models
{
    public class Sponsorship
    {
        [Key]
        public int SponsorshipID { get; set; }
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public string? Note { get; set; }

        public ApplicationUser User { get; set; }
        public Device Device { get; set; }
        public Location Location { get; set; }
    }

}
