namespace ProViewGolf.Core.Dbo.Models
{
    public enum Duration
    {
        Week = 1,
        Month = 2,
        Year = 3
    }

    public class ShotStatModel
    {
        public int Shots { get; set; }
        public int GoodShots { get; set; }
        public string XAxis { get; set; }
        public int YAxis { get; set; }
    }
}
