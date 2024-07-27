namespace System.MVC.ViewModels
{
    public class SponsorshipViewModel
    {
        public int SponsorshipID { get; set; }
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? Note { get; set; }
    }


}
