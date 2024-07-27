using System.ComponentModel.DataAnnotations;

namespace System.DAL.Models
{
    public class DeviceCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public ICollection<Device> Devices { get; set; }
    }

}
