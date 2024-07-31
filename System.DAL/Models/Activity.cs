namespace System.DAL.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime time { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
