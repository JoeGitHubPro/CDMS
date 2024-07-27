using System.DAL.Models;

namespace System.MVC.ViewModels
{
    public class DeviceViewModel
    {
        public string DeviceID { get; set; }
        public int DeviceCategory { get; set; }
        public string DeviceName { get; set; }
        public int? DeviceSpecificationId { get; set; }
        public DeviceSpecifications? DeviceSpecification { get; set; }

        // Additional fields for display purposes
        public string? DeviceCategoryName { get; set; }
    }

}
