namespace System.MVC.ViewModels
{
    public class DeviceViewModel
    {
        public string ID { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string? INFO { get; set; }

        // Additional fields for display purposes
        public string? CategoryName { get; set; }
    }

}
