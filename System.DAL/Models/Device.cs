using System.ComponentModel.DataAnnotations;

namespace System.DAL.Models
{
    public class Device
    {
        [Key]
        public string DeviceID { get; set; }
        public int DeviceCategory { get; set; }
        public string DeviceName { get; set; }
        public string? DeviceINFO { get; set; }

        public DeviceCategory Category { get; set; }
    }

}
