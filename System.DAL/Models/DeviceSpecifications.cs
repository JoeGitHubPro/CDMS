using System.ComponentModel.DataAnnotations;

namespace System.DAL.Models
{
    public class DeviceSpecifications
    {
        [Key]
        public int SpecificationID { get; set; }
        public string ModelName { get; set; }
        public string? INFO { get; set; }
    }

}
